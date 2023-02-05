using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public class InitBankTransactionModel
    {
        public long TransactionId { get; }
        public long AccountNo { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; }
        public long Amount { get; set; }

        public InitBankTransactionModel()
        {

        }

        public InitBankTransactionModel(long accountNo, string type, long amount)
        {
            AccountNo = accountNo;
            Type = type;
            Amount = amount;
        }
    }
}
