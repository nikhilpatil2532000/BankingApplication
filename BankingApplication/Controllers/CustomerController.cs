using BankingApplication.Models;
using BankingApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepo _custRepo;

        public CustomerController(ICustomerRepo custRepo)
        {
            _custRepo = custRepo;
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(InitAddNewCustomerModel newCustomerModel)
        {
            return Ok(_custRepo.AddCustomer(newCustomerModel));
        }

        [HttpPost("AddBulkCustomer")]
        public IActionResult AddBulkCustomer(List<InitAddNewCustomerModel> listOfnewCustomerModel)
        {
            string message = "";
            foreach (InitAddNewCustomerModel i in listOfnewCustomerModel)
            {
                message += _custRepo.AddCustomer(i);
            }
            return Ok(message);
        }

        [HttpGet("GetCustomerByCustomerId")]
        public IActionResult GetCustomerByCustomerId(long customerId)
        {
            return Ok(_custRepo.GetCustomerByCustomerId(customerId));
        }

        [HttpGet("GetAllCustomer")]
        public IActionResult GetAllCustomer()
        {
            return Ok(_custRepo.GetAllCustomer());
        }

        [HttpPatch("UpdateCustomerDetails")]
        public IActionResult UpdateCustomerDetails(long customerId, JsonPatchDocument bankCustomerModel)
        {
            return Ok(_custRepo.UpdateCustomerDetails(customerId, bankCustomerModel));
        }

        [HttpDelete("DeleteCustomerByCustomerId")]
        public IActionResult DeleteCustomerByCustomerId(long customerId)
        {
            return Ok(_custRepo.DeleteCustomerByCustomerId(customerId).Item2);
        }
    }
}
