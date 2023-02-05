using AutoMapper;
using BankingApplication.AccountInterest;
using BankingApplication.DAL;
using BankingApplication.IdProvider;
using BankingApplication.Models;
using BankingApplication.StaticValues;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private SmallOfficeContext _context;
        private IMapper _mapper;
        private IServiceProvider _serviceProvider;
        public AccountRepo(SmallOfficeContext context, IMapper mapper,IServiceProvider serviceProvider)
        {
            _context = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public bool CheckAccountIsPresent(long accountNumber)
        {
            var bankAccount = _context.BankAccounts.Find(accountNumber);
            if (bankAccount != null)
            {
                return true;
            }
            return false;
        }

        public string AddAccount(InitBankAccountModel initBankAccountModel)
        {
            string message = "";
            IGenerateID gen = _serviceProvider.GetRequiredService<IGenerateID>();
            ICustomerRepo customerRepo = _serviceProvider.GetRequiredService<ICustomerRepo>();
            ITransactionRepo transactionRepo = _serviceProvider.GetRequiredService<ITransactionRepo>();

            if (customerRepo.CheckCustomerIsPresent(initBankAccountModel.CustomerId))
            {
                BankAccountModel bankAccountModel = _mapper.Map<BankAccountModel>(initBankAccountModel);
                bankAccountModel.AccountNo = gen.Generator16DigitUniqueNumber();
                bankAccountModel.ActivationDate = DateTime.Now;
                bankAccountModel.TotalBalance = 0;
                _context.BankAccounts.Add(_mapper.Map<BankAccount>(bankAccountModel));
                _context.SaveChanges();
                message += "Account is Added\n";

                InitBankTransactionModel bankTransaction
                    = new InitBankTransactionModel(bankAccountModel.AccountNo, "credit", initBankAccountModel.TotalBalance);
                message += transactionRepo.AddTransaction(bankTransaction).Item2 + "\n";
            }
            else
            {
                message += "Customer ID is not found\n";
            }
            return message;
        }

        public BankAccountModel GetAccountByAccountNumber(long accountNumber)
        {
            return _mapper.Map<BankAccountModel>(_context.BankAccounts.Find(accountNumber));
        }

        public List<BankAccountModel> GetAccountByCustomerId(long customerId)
        {
            var temp = _context.BankAccounts.Where(x => x.CustomerId == customerId).ToList();
            return _mapper.Map<List<BankAccountModel>>(temp);
        }

        public List<BankAccountModel> GetAllAccounts()
        {
            return _mapper.Map<List<BankAccountModel>>(_context.BankAccounts.Select(x => x).ToList());
        }

        public string GetAccountTransactionSummary(long accountNumber, int numberOfTransaction)
        {
            ITransactionRepo transactionRepo = _serviceProvider.GetRequiredService<ITransactionRepo>();
            if (CheckAccountIsPresent(accountNumber))
            {
                return transactionRepo.TransactionSummary(accountNumber, numberOfTransaction);
            }
            return "Account is not found";
        }

        public string GetAccountTransactionSummaryByDate(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            ITransactionRepo transactionRepo = _serviceProvider.GetRequiredService<ITransactionRepo>();
            if (CheckAccountIsPresent(accountNumber))
            {
                return transactionRepo.TransactionSummaryByDate(accountNumber,fromDate,toDate);
            }
            return "Account is not found";
        }

        public double GetInterestByAccountNumber(long bankAccountNumber,int numberOfDays)
        {
            Func<string,IAccount> iAccount = _serviceProvider.GetRequiredService<Func<string, IAccount>>();
            BankAccountModel bankAccountModel = GetAccountByAccountNumber(bankAccountNumber);
            DateTime fromDate = DateTime.Now.Date - TimeSpan.FromDays(numberOfDays);
            DateTime toDate = DateTime.Now.Date;
            return iAccount(bankAccountModel.Type).TotalInterest(bankAccountModel,fromDate,toDate);
        }

        public string UpdateAccountDetails(long accountNumber, JsonPatchDocument bankAccountModel)
        {
            var accModel = _context.BankAccounts.Where(x => x.AccountNo == accountNumber).FirstOrDefault();
            if (accModel != null)
            {
                bankAccountModel.ApplyTo(accModel);
                _context.SaveChanges();
                return "Account Details updated";
            }
            else
            {
                return "Account is not found";
            }
        }

        public (bool, string) DeleteAccountByAccountNumber(long accountNumber)
        {
            string message = "";
            ITransactionRepo transactionRepo = _serviceProvider.GetRequiredService<ITransactionRepo>();
            var temp = transactionRepo.DeleteTransactionByAccountNumber(accountNumber);
            BankAccount bankAccount;
            if (temp.Item1)
            {
                message += temp.Item2;
                bankAccount = _context.BankAccounts.Find(accountNumber);
                if (bankAccount == null)
                {
                    message += "Account is already deleted";
                    return (false, message);
                }
                _context.BankAccounts.Remove(bankAccount);
                _context.SaveChanges();
                message += "Account Number "+bankAccount.AccountNo+" is deleted sucessfully\n";
                int countOfAccounts = (from i in _context.BankAccounts
                                       where i.CustomerId == bankAccount.CustomerId
                                       select i).Count();
                if (countOfAccounts == 0)
                {
                    BankCustomer bankCustomer = _context.BankCustomers.Find(bankAccount.CustomerId);
                    _context.BankCustomers.Remove(bankCustomer);
                    _context.SaveChanges();
                    message += "Customer ID "+bankCustomer.CustomerId+" is deleted sucessfully";
                }
            }
            return (true, message);
        }
    }
}
