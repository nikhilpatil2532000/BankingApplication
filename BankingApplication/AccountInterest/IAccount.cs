using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.AccountInterest
{
    public interface IAccount
    {
        (bool, string) CheckTransactionIsValid(BankTransactionModel bankTransactionModel);
        double TotalInterest(BankAccountModel bankAccountModel,DateTime fromDate,DateTime toDate);
    }
}
