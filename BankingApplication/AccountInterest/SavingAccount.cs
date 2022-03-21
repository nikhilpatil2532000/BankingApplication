using AutoMapper;
using BankingApplication.DAL;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.AccountInterest
{
    public class SavingAccount : IAccount
    {
        //maximum 4 transaction allow 
        private static readonly long MAX_TRANSACTION = 4;

        //maximum 50k ammount can withdrawn
        private static readonly long MAX_AMOUNT = 50000;

        private static readonly int rateOfInterest = 4;

        private SmallOfficeContext _context;
        private IMapper _mapper;
        public SavingAccount(SmallOfficeContext context,IMapper mapper)
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
                if (bankAccount.TotalBalance - bankTransactionModel.Amount < 3000)
                {
                    return (false, "Minimum Rs.3000 balance is required in Account\n Transaction failed\nTransaction failed");
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
            interest += (4 / 100) * (timeSpan.TotalDays / 365) * (closingBalance);
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
                //fix
                if (i<listTran.Count-1)
                {
                    timeSpan = listTran[i].Date - listTran[i + 1].Date;
                }
                else
                {
                    timeSpan = listTran[i].Date - fromDate;
                }
                interest += (4 / 100) * (timeSpan.TotalDays / 365) * (closingBalance);
            }
            interest += (4 / 100) * (timeSpan.TotalDays / 365) * (closingBalance);
            return interest;
        }
    }
}
