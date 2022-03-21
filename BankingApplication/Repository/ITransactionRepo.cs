using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Repository
{
    public interface ITransactionRepo
    {
        (bool, string) CheckTransactionIsValid(BankTransactionModel bankTransactionModel);
        (bool, string) PerformTransaction(BankTransactionModel bankTransactionModel);
        (bool, string) AddTransaction(InitBankTransactionModel tranModel);
        string AddTransferTransaction(long fromAccountNumber, long toAccountNumber, long amount);
        string TransactionSummary(long accountNumber, int numberOfTransaction);
        (bool, string) DeleteTransactionByAccountNumber(long accountNumber);
    }
}
