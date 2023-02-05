using AutoMapper;
using BankingApplication.DAL;
using BankingApplication.Models;
using BankingApplication.StaticValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.AccountInterest
{
    public class CurrentAccount : IAccount
    {
        private SmallOfficeContext _context;

        private static readonly long MAX_TRANSACTION = Current.MAX_TRANSACTION;

        private static readonly long MAX_AMOUNT = Current.MAX_AMOUNT;

        private static readonly double rateOfInterest = Current.rateOfInterest;

        private IMapper _mapper;

        public CurrentAccount(SmallOfficeContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public (bool, string) CheckTransactionIsValid(BankTransactionModel bankTransactionModel)
        {
            if (bankTransactionModel.Type == "debit")
            {
                var bankAccount = _context.BankAccounts.Find(bankTransactionModel.AccountNo);
                var listOfBankTransactionModels = (from i in _context.BankTransactions
                                                   where i.AccountNo == bankAccount.AccountNo
                                                   && i.Type == "debit" && i.Date.Date == DateTime.Now.Date
                                                   select _mapper.Map<BankTransactionModel>(i)).ToList();
                if (listOfBankTransactionModels.Count > MAX_TRANSACTION)
                {
                    return (false, "Maximum transaction reached\nTransaction failed");
                }
                if (listOfBankTransactionModels.Sum(i => i.Amount) + bankTransactionModel.Amount > MAX_AMOUNT)
                {
                    return (false, "Maximum withdrawal amount reached\nTransaction failed");
                }
                if (bankAccount.TotalBalance < bankTransactionModel.Amount)
                {
                    return (false, "Don't have enough money in your bank account\nTransaction failed");
                }
            }
            return (true, "\nTransaction completed");
        }

        public double TotalInterest(BankAccountModel bankAccountModel,DateTime fromDate, DateTime toDate)
        {
            return 0;
        }

    }
}
