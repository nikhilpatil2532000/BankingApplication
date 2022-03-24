using AutoMapper;
using BankingApplication.DAL;
using BankingApplication.IdProvider;
using BankingApplication.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private SmallOfficeContext _context;
        private IMapper _mapper;
        private IServiceProvider _serviceProvider;
        public CustomerRepo(SmallOfficeContext context,IMapper mapper, IServiceProvider serviceProvider)
        {
            _context = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public bool CheckCustomerIsPresent(long customerId)
        {
            BankCustomer bankCustomer = _context.BankCustomers.Find(customerId);
            if (bankCustomer != null)
            {
                return true;
            }
            return false;
        }

        public string AddCustomer(InitAddNewCustomerModel newCustomer)
        {
            string message = "";
            IGenerateID gen = _serviceProvider.GetRequiredService<IGenerateID>();
            IAccountRepo accountRepo = _serviceProvider.GetRequiredService<IAccountRepo>();

            if (newCustomer.Type == "Saving" && newCustomer.TotalBalance >= 3000)
            {
                AddNewCustomerModel addNewCustomerModel = _mapper.Map<AddNewCustomerModel>(newCustomer);
                addNewCustomerModel.CustomerId = gen.Generator4DigitUniqueNumber();
                addNewCustomerModel.CreationDate = DateTime.Now;
                addNewCustomerModel.ActivationDate = DateTime.Now;

                BankCustomerModel bankCustomerModel = _mapper.Map<BankCustomerModel>(addNewCustomerModel);
                _context.BankCustomers.Add(_mapper.Map<BankCustomer>(bankCustomerModel));
                _context.SaveChanges();
                message += "Customer is Added\n";
                InitBankAccountModel initBankAccountModel
                    = new InitBankAccountModel(addNewCustomerModel.CustomerId, addNewCustomerModel.Type, addNewCustomerModel.TotalBalance, addNewCustomerModel.Status);

                message += accountRepo.AddAccount(initBankAccountModel) + "\n";
            }
            else
            {
                message += "Minimum 3000 balance required\n";
            }
            return message;
        }

        public List<BankCustomerModel> GetCustomerByCustomerId(long customerId)
        {
            var temp = _context.BankCustomers.Where(x => x.CustomerId == customerId).ToList();
            return _mapper.Map<List<BankCustomerModel>>(temp);
        }

        public List<BankCustomerModel> GetAllCustomer()
        {
            return _mapper.Map<List<BankCustomerModel>>(_context.BankCustomers.Select(x => x).ToList());
        }

        public string UpdateCustomerDetails(long customerId,JsonPatchDocument bankCustomerModel)
        {
            var custModel = _context.BankCustomers.Find(customerId);
            if (custModel != null)
            {
                bankCustomerModel.ApplyTo(custModel);
                _context.SaveChanges();
                return "Customer Details updated";
            }
            else
            {
                return "Customer is not found";
            }
        }

        public (bool, string) DeleteCustomerByCustomerId(long customerId)
        {
            string message = "";
            IAccountRepo accountRepo = _serviceProvider.GetRequiredService<IAccountRepo>();
            if (CheckCustomerIsPresent(customerId))
            {
                List<long> listOfAccountNumber = (from i in accountRepo.GetAccountByCustomerId(customerId) select i.AccountNo).ToList();
                foreach (long i in listOfAccountNumber)
                {
                    message += accountRepo.DeleteAccountByAccountNumber(i).Item2;
                }
                BankCustomer bankCustomer = (from i in _context.BankCustomers
                                             where i.CustomerId == customerId
                                             select i).FirstOrDefault();
                _context.BankCustomers.Remove(bankCustomer);
                _context.SaveChanges();
                message += "Customer deleted sucessfully\n";
            }
            else
            {
                message += "Customer not found\n";
            }
            return (true,message);
        }
    }
}
