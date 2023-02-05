using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public class InitAddNewCustomerModel
    {
        public long CustomerId { get; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PanCard { get; set; }
        public string AadharCard { get; set; }
        public DateTime? CreationDate { get;}

        public long AccountNo { get; }
        public string Type { get; set; }
        public long TotalBalance { get; set; }
        public string Status { get; set; }
        public DateTime ActivationDate { get;}

        public InitAddNewCustomerModel(string firstName, string middleName, 
            string lastName,string mobile, string email, string gender, string address,
            string panCard, string aadharCard,string type, long totalBalance, string status)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Mobile = mobile;
            Email = email;
            Gender = gender;
            Address = address;
            PanCard = panCard;
            AadharCard = aadharCard;
            
            Type = type;
            TotalBalance = totalBalance;
            Status = status;
        }
    }
}
