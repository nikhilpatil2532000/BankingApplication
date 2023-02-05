using BankingApplication.Models;
using BankingApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionRepo _tranRepo;

        public TransactionController(ITransactionRepo tranRepo)
        {
            _tranRepo = tranRepo;
        }

        [HttpPost("AddTransaction")]
        public IActionResult AddTransaction(InitBankTransactionModel tranModel)
        {
            return Ok(_tranRepo.AddTransaction(tranModel).Item2);
        }

        [HttpPost("AddTransferTransaction")]
        public IActionResult AddTransferTransaction(long fromAccountNumber, long toAccountNumber, long amount)
        {
            return Ok(_tranRepo.AddTransferTransaction(fromAccountNumber,toAccountNumber,amount));
        }
    }
}
