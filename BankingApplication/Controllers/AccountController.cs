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
    public class AccountController : ControllerBase
    {
        private IAccountRepo _accRepo;

        public AccountController(IAccountRepo accRepo)
        {
            _accRepo = accRepo;
        }

        [HttpPost("AddAccount")]
        public IActionResult AddAccount(InitBankAccountModel accModel)
        {
            return Ok(_accRepo.AddAccount(accModel));
        }

        [HttpGet("GetAccountByAccountNumber")]
        public IActionResult GetAccountByAccountNumber(long accountNumber)
        {
            return Ok(_accRepo.GetAccountByAccountNumber(accountNumber));
        }

        [HttpGet("GetAccountByCustomerId")]
        public IActionResult GetAccountByCustomerId(long customerId)
        {
            return Ok(_accRepo.GetAccountByCustomerId(customerId));
        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            return Ok(_accRepo.GetAllAccounts());
        }

        [HttpGet("GetAccountTransactionSummary")]
        public IActionResult GetAccountTransactionSummary(long accountNumber,int numberOfTransaction)
        {
            return Ok(_accRepo.GetAccountTransactionSummary(accountNumber, numberOfTransaction));
        }

        [HttpGet("GetInterestByAccountNumber")]
        public IActionResult GetInterestByAccountNumber(long bankAccountNumber, int numberOfDays)
        {
            return Ok("Total interest is " + _accRepo.GetInterestByAccountNumber(bankAccountNumber,numberOfDays));
        }

        [HttpPatch("UpdateAccountDetails")]
        public IActionResult UpdateAccountDetails(long accountNumber, JsonPatchDocument bankAccountModel)
        {
            return Ok(_accRepo.UpdateAccountDetails(accountNumber, bankAccountModel));
        }

        [HttpDelete("DeleteAccountByAccountNumber")]
        public IActionResult DeleteAccountByAccountNumber(long accountNumber)
        {
            return Ok(_accRepo.DeleteAccountByAccountNumber(accountNumber).Item2);
        }
    }
}
