using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public class BankAccountModel
    {
        public long AccountNo { get; set; }
        public long CustomerId { get; set; }
        public string Type { get; set; }
        public long TotalBalance { get; set; }
        public string Status { get; set; }
        public DateTime ActivationDate { get; set; }

        public BankAccountModel()
        {

        }

        public BankAccountModel(long accountNo,long customerId,string type,
                            long totalBalance,string status,DateTime activationDate)
        {
            AccountNo = accountNo;
            CustomerId = customerId;
            Type = type;
            TotalBalance = totalBalance;
            Status = status;
            ActivationDate = activationDate;
        }
    }
}
