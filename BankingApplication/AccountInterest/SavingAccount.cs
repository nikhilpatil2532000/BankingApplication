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
    public class SavingAccount : IAccount
    {
        //maximum 4 transaction allow 
        private static readonly long MAX_TRANSACTION = Saving.MAX_TRANSACTION;

        //maximum 50k ammount can withdrawn
        private static readonly long MAX_AMOUNT = Saving.MAX_AMOUNT;

        private static readonly double rateOfInterest = Saving.rateOfInterest;

        private static readonly long MINIMUM_BALANCE = Saving.MINIMUM_BALANCE;

        private SmallOfficeContext _context;
        private IMapper _mapper;
        public SavingAccount(SmallOfficeContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public (bool, string) CheckTransactionIsValid(BankTransactionModel bankTransactionModel)
        { 
            var bankAccount = _context.BankAccounts.Find(bankTransactionModel.AccountNo);
            if (bankTransactionModel.Type == "debit" )
            {
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
                if (bankAccount.TotalBalance - bankTransactionModel.Amount < MINIMUM_BALANCE)
                {
                    return (false, "Minimum Rs."+MINIMUM_BALANCE+" balance is required in Account\nTransaction failed");
                }
            }
            return (true, "\nTransaction completed");
        }

        public double TotalInterest(BankAccountModel bankAccountModel, DateTime fromDate, DateTime toDate)
        {
            List<BankTransactionModel> listTran = (from i in _context.BankTransactions
                                               where i.AccountNo == bankAccountModel.AccountNo
                                               && i.Date.Date >= fromDate && i.Date.Date <=toDate
                                               orderby i.Date descending
                                               select _mapper.Map<BankTransactionModel>(i)).ToList();

            long closingBalance = bankAccountModel.TotalBalance;
            double interest = 0;

            TimeSpan timeSpan = toDate - listTran[0].Date;
            interest += (rateOfInterest / 100.0) * (Math.Abs(timeSpan.TotalDays) / 365.0) * (closingBalance);
            for (int i = 0; i < listTran.Count; i++)
            {
                if (listTran[i].Type == "credit")
                {
                    closingBalance += listTran[i].Amount;
                }
                else if (listTran[i].Type == "debit")
                {
                    closingBalance -= listTran[i].Amount;
                }

                if (i<listTran.Count-1)
                {
                    timeSpan = listTran[i].Date - listTran[i + 1].Date;
                }
                else
                {
                    timeSpan = listTran[i].Date - fromDate;
                }
                interest += (4 / 100.0) * (Math.Abs(timeSpan.TotalDays) / 365.0) * (closingBalance);
            }
            return interest;
        }
    }
}
