using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public class InitBankAccountModel
    {
        public long AccountNo { get; }
        public long CustomerId { get; set; }
        public string Type { get; set; }
        public long TotalBalance { get; set; }
        public string Status { get; set; }
        public DateTime ActivationDate { get; }

        public InitBankAccountModel()
        {

        }

        public InitBankAccountModel(long customerId, string type, long totalBalance, string status)
        {
            CustomerId = customerId;
            Type = type;
            TotalBalance = totalBalance;
            Status = status;
        }
    }
}
