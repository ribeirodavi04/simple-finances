﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleFinances.Application.Services;
using SimpleFinances.Application.Services.AutoMapper;
using SimpleFinances.Application.Services.Cryptography;
using SimpleFinances.Application.UseCases.Card.Delete;
using SimpleFinances.Application.UseCases.Card.GetAll;
using SimpleFinances.Application.UseCases.Card.GetById;
using SimpleFinances.Application.UseCases.Card.Register;
using SimpleFinances.Application.UseCases.Card.Update;
using SimpleFinances.Application.UseCases.Income.Register;
using SimpleFinances.Application.UseCases.Login.DoLogin;
using SimpleFinances.Application.UseCases.User.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEncripter(services, configuration);
        }

        public static void AddAutoMapper(IServiceCollection services) 
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services) 
        { 
            //user
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

            //cards
            services.AddScoped<IRegisterCardUseCase, RegisterCardUseCase>();
            services.AddScoped<IUpdateCardUseCase, UpdateCardUseCase>();
            services.AddScoped<IGetCardByIdUseCase, GetCardByIdUseCase>();
            services.AddScoped<IDeleteCardUseCase, DeleteCardUseCase>();
            services.AddScoped<IGetAllCardsUseCase, GetAllCardsUseCase>();

            //incomes
            services.AddScoped<IRegisterIncomeUseCase, RegisterIncomeUseCase>();
        }

        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
            services.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
