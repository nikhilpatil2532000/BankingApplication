using AutoMapper;
using BankingApplication.AccountInterest;
using BankingApplication.DAL;
using BankingApplication.IdProvider;
using BankingApplication.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private SmallOfficeContext _context;
        private IMapper _mapper;
        private IServiceProvider _serviceProvider;
        public TransactionRepo(SmallOfficeContext context, IMapper mapper,IServiceProvider serviceProvider)
        {
            _context = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public (bool,string) CheckTransactionIsValid(BankTransactionModel bankTransactionModel)
        {
            var typeOfAccount = _serviceProvider.GetRequiredService<Func<string, IAccount>>();
            string accountType = (from i in _context.BankAccounts 
                                 where i.AccountNo == bankTransactionModel.AccountNo 
                                 select i.Type).FirstOrDefault();
            return typeOfAccount(accountType).CheckTransactionIsValid(bankTransactionModel);
        }

        public (bool,string) PerformTransaction(BankTransactionModel bankTransactionModel)
        {
            string message = "";
            (bool flag, string newMessage) = CheckTransactionIsValid(bankTransactionModel);
            if (flag)
            {
                var temp = _context.BankAccounts.Find(bankTransactionModel.AccountNo);
                if (bankTransactionModel.Type == "credit")
                {
                    temp.TotalBalance = temp.TotalBalance + bankTransactionModel.Amount;
                    _context.BankAccounts.Update(temp);
                    _context.SaveChanges();
                    message += "Rs." + bankTransactionModel.Amount + " will be credited in Account No." + bankTransactionModel.AccountNo + "\n";
                    message += "Total Balance :" + temp.TotalBalance + "\n";
                }
                else if (bankTransactionModel.Type == "debit")
                {
                    temp.TotalBalance = temp.TotalBalance - bankTransactionModel.Amount;
                    _context.BankAccounts.Update(temp);
                    _context.SaveChanges();
                    message += "Rs." + bankTransactionModel.Amount + " will be debited from Account No." + bankTransactionModel.AccountNo + "\n";
                    message += "Total Balance :" + temp.TotalBalance + "\n";
                }
                _context.BankTransactions.Add(_mapper.Map<BankTransaction>(bankTransactionModel));
                _context.SaveChanges();
            }
            else
            {
                message += newMessage;
            }
            return (flag,message);
        }

        public (bool,string) AddTransaction(InitBankTransactionModel initBankTransactionModel)
        {
            string message = "";
            IAccountRepo accountRepo = _serviceProvider.GetRequiredService<IAccountRepo>();
            IGenerateID generateID = _serviceProvider.GetRequiredService<IGenerateID>();
            if (accountRepo.CheckAccountIsPresent(initBankTransactionModel.AccountNo))
            {
                BankTransactionModel bankTransactionModel = _mapper.Map<BankTransactionModel>(initBankTransactionModel);
                bankTransactionModel.TransactionId = generateID.GeneratorTransactionID();
                bankTransactionModel.Date = DateTime.Now;
                (bool checkValid,string newMessage) = PerformTransaction(bankTransactionModel);
                message += newMessage;
                return (checkValid, message);
            }
            else
            {
                message += "Account number is not found\n";
                return (false, message);
            }
        }

        public string AddTransferTransaction(long fromAccountNumber, long toAccountNumber, long amount)
        {
            string message = "";
            IAccountRepo accountRepo = _serviceProvider.GetRequiredService<IAccountRepo>();
            IGenerateID generateID = _serviceProvider.GetRequiredService<IGenerateID>();
            bool isFromAccountIsPresent = accountRepo.CheckAccountIsPresent(fromAccountNumber);
            bool isToAccountIsPresent = accountRepo.CheckAccountIsPresent(toAccountNumber);
            if ( isFromAccountIsPresent && isToAccountIsPresent)
            {
                long commonId = generateID.GeneratorTransactionID();
                BankTransactionModel sender = new BankTransactionModel()
                {
                    TransactionId = commonId,
                    AccountNo = fromAccountNumber,
                    Type = "debit",
                    Amount = amount,
                    Date = DateTime.Now
                };

                BankTransactionModel receiver = new BankTransactionModel()
                {
                    TransactionId = commonId,
                    AccountNo = toAccountNumber,
                    Type = "credit",
                    Amount = amount,
                    Date = DateTime.Now
                };
                (bool flag1, string newMessage1) = PerformTransaction(sender);
                message += newMessage1;
                if (flag1)
                {
                    (bool flag2, string newMessage2) = PerformTransaction(receiver);
                    message += newMessage2;
                }
            }
            if(!isFromAccountIsPresent)
            {
                message += "From Account is not found\nTransaction failed";
            }
            if (!isToAccountIsPresent)
            {
                message += "To Account is not found\nTransaction failed";
            }
            return message;
        }

        public string TransactionSummary(long accountNumber,int numberOfTransaction)
        {
            string summary = "";
            List<BankTransactionModel> listOfTransactions = (_context.BankTransactions
                                          .Where(i => i.AccountNo==accountNumber)
                                          .OrderBy(i => i.Date).Take(numberOfTransaction)
                                          .Select(i => _mapper.Map<BankTransactionModel>(i))).ToList();
            foreach (BankTransactionModel i in listOfTransactions)
            {
                summary += i.ToString();
            }
            return summary;
        }

        public string TransactionSummaryByDate(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            string summary = "";
            List<BankTransactionModel> listOfTransactions = (_context.BankTransactions
                                          .Where(i => i.AccountNo == accountNumber 
                                          && i.Date.Date>=fromDate.Date && i.Date.Date<=toDate.Date)
                                          .Select(i => _mapper.Map<BankTransactionModel>(i))).ToList();
            foreach (BankTransactionModel i in listOfTransactions)
            {
                summary += i.ToString();
            }
            return summary;
        }

        public (bool, string) DeleteTransactionByAccountNumber(long accountNumber)
        {
            var allTransaction = (from i in _context.BankTransactions
                                  where i.AccountNo == accountNumber
                                  select i).ToList();
            _context.BankTransactions.RemoveRange(allTransaction);
            _context.SaveChanges();
            return (true, "All Transactions of account number "+accountNumber+" is deleted sucessfully\n");
        }
    }
}
