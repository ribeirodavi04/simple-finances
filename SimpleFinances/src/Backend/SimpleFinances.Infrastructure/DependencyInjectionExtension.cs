using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories.Income;
using SimpleFinances.Domain.Repositories.User;
using SimpleFinances.Domain.Security.Tokens;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Infrastructure.Context;
using SimpleFinances.Infrastructure.DataAccess;
using SimpleFinances.Infrastructure.DataAccess.Repositories;
using SimpleFinances.Infrastructure.Extensions;
using SimpleFinances.Infrastructure.Security.Tokens.Access.Generator;
using SimpleFinances.Infrastructure.Security.Tokens.Access.Validator;
using SimpleFinances.Infrastructure.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddTokens(services,configuration);
            AddLoggedUser(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddDbContext<SimpleFinancesDbContext>(dbContextOptions => dbContextOptions.UseNpgsql(connectionString));
        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration) 
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

            services.AddScoped<ICardReadOnlyRepository, CardRepository>();
            services.AddScoped<ICardWriteOnlyRepository, CardRepository>();
            services.AddScoped<ICardUpdateOnlyRepository, CardRepository>();

            services.AddScoped<IIncomeReadOnlyRepository, IncomeRepository>();
            services.AddScoped<IIncomeWriteOnlyRepository, IncomeRepository>();
            services.AddScoped<IIncomeUpdateOnlyRepository, IncomeRepository>();
        }

        private static void AddLoggedUser(IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();
        }
    }
}
