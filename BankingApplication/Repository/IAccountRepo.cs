using BankingApplication.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Repository
{
    public interface IAccountRepo
    {
        bool CheckAccountIsPresent(long accountNumber);
        string AddAccount(InitBankAccountModel accountModel);
        BankAccountModel GetAccountByAccountNumber(long accountNumber);
        List<BankAccountModel> GetAccountByCustomerId(long customerId);
        List<BankAccountModel> GetAllAccounts();
        string GetAccountTransactionSummary(long accountNumber, int numberOfTransaction);
        string GetAccountTransactionSummaryByDate(long accountNumber, DateTime fromDate, DateTime toDate);
        double GetInterestByAccountNumber(long bankAccountNumber, int numberOfDays);
        string UpdateAccountDetails(long accountNumber, JsonPatchDocument bankAccountModel);
        (bool, string) DeleteAccountByAccountNumber(long accountNumber);
    }
}
