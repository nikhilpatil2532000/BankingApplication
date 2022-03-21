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
            CreateMap<InitBankAccountModel, BankAccountModel>()
            .ForMember(dest => dest.AccountNo, opt => opt.MapFrom(src => src.AccountNo))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.TotalBalance, opt => opt.MapFrom(src => src.TotalBalance))
            .ForMember(dest => dest.ActivationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            //customers
            //required
            CreateMap<BankCustomer, BankCustomerModel>().ReverseMap();

            //required
            CreateMap<InitAddNewCustomerModel, AddNewCustomerModel>();

            //required
            CreateMap<AddNewCustomerModel, BankCustomerModel>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PanCard, opt => opt.MapFrom(src => src.PanCard))
            .ForMember(dest => dest.AadharCard, opt => opt.MapFrom(src => src.AadharCard))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));

            //required
            CreateMap<AddNewCustomerModel, BankTransactionModel>()
            .ForMember(dest => dest.AccountNo, opt => opt.MapFrom(src => src.AccountNo))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TotalBalance))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));

            //required
            CreateMap<AddNewCustomerModel, BankAccountModel>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.TotalBalance, opt => opt.MapFrom(src => src.TotalBalance))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ActivationDate, opt => opt.MapFrom(src => DateTime.Now));

            //Transaction
            //required
            CreateMap<BankTransaction, BankTransactionModel>().ReverseMap();

            //required
            CreateMap<InitBankTransactionModel, BankTransactionModel>()
            .ForMember(dest => dest.AccountNo, opt => opt.MapFrom(src => src.AccountNo))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));



        }

    }
}
