using AutoMapper;
using BankingApplication.DAL;
using BankingApplication.IdProvider;
using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.AutoMappers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //Accounts
            //required
            CreateMap<BankAccount, BankAccountModel>().ReverseMap();

            //required
            CreateMap<InitBankAccountModel, BankAccountModel>();

            //customers
            //required
            CreateMap<BankCustomer, BankCustomerModel>().ReverseMap();

            //required
            CreateMap<InitAddNewCustomerModel, BankCustomerModel>();

            //Transaction
            //required
            CreateMap<BankTransaction, BankTransactionModel>().ReverseMap();

            //required
            CreateMap<InitBankTransactionModel, BankTransactionModel>();
        }

    }
}
