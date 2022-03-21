using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public class BankTransactionModel
    {
        public long TransactionId { get; set; }
        public long AccountNo { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public long Amount { get; set; }

        public BankTransactionModel()
        {

        }

        public BankTransactionModel(long transactionId, long accountNo, string type, DateTime date, long amount)
        {
            TransactionId = transactionId;
            AccountNo = accountNo;
            Type = type;
            Date = date;
            Amount = amount;
        }

        public override string ToString()
        {
            return Amount + " is " + Type + " " + " on "+Date+"\n"; 
        }
    }
}
