using BankingApplication.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Repository
{
    public interface ICustomerRepo
    {
        bool CheckCustomerIsPresent(long customerId);
        string AddCustomer(InitAddNewCustomerModel newCustomer);
        List<BankCustomerModel> GetCustomerByCustomerId(long customerId);
        List<BankCustomerModel> GetAllCustomer();
        string UpdateCustomerDetails(long customerId, JsonPatchDocument bankCustomerModel);
        public (bool, string) DeleteCustomerByCustomerId(long customerId);
    }
}
