using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Income;
using Common.TestUtilities.Requests;
using SimpleFinances.Application.UseCases.Income.Update;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using SimpleFinances.Infrastructure.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Income.Update
{
    public class UpdateIncomeUseCaseTest
    {
        private UpdateIncomeUseCase CreateUseCase(User user, int idIncome, SimpleFinances.Domain.Entities.Income income, bool incomeFound = true, string? incomeTypeName = null)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unityOfWork = UnityOfWorkBuilder.Build();
            var readRepository = new IncomeReadOnlyRepositoryBuilder();
            var updateRepository = new IncomeUpdateOnlyRepositoryBuilder();

            if (incomeTypeName is not null)
                readRepository.ExistIncomeTypeName(incomeTypeName, user.UserId);

            if (incomeFound)
                updateRepository.GetIncomeById(idIncome, user, income);
            else
                updateRepository.GetIncomeById(idIncome, user, null!);

            return new UpdateIncomeUseCase(updateRepository.Build(), readRepository.Build(), mapper, unityOfWork, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var request = RequestIncomeJsonBuilder.Build();
            var user = new UserBuilder().Build();
            var income = IncomeBuilder.Build(user);

            var useCase = CreateUseCase(user, income.IncomeId, income);

            //Act
            Func<Task> act = async () => await useCase.Execute(income.IncomeId, request);
            await act();

            //Assert
            Assert.Equal(request.TypeName, income.TypeName);
            Assert.Equal(request.Amount, income.Amount);
            Assert.Equal(request.DateReceived, income.DateReceived);
            Assert.Equal(request.IsRecurring, income.IsRecurring);
        }

        [Fact]
        public async Task Error_Income_Type_Name_Empty()
        {
            //Arrange
            var request = RequestIncomeJsonBuilder.Build();
            request.TypeName = string.Empty;
            var user = new UserBuilder().Build();
            var income = IncomeBuilder.Build(user);

            var useCase = CreateUseCase(user, income.IncomeId, income);

            //Act
            Func<Task> act = async () => await useCase.Execute(income.IncomeId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.INCOME_TYPE_NAME_EMPTY, exception.ErrorsMessages);
        }

        [Fact]
        public async Task Error_Income_Not_Found()
        {
            //Arrange
            var request = RequestIncomeJsonBuilder.Build();
            var user = new UserBuilder().Build();
            var income = IncomeBuilder.Build(user);

            var useCase = CreateUseCase(user, income.IncomeId, income, incomeFound: false);

            //Act 
            Func<Task> act = async () => await useCase.Execute(income.IncomeId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.INCOME_NOT_FOUND, exception.Message);
        }

        [Fact]
        public async Task Error_Income_TypeName_Already_Registered()
        {
            //Arrange
            var request = RequestIncomeJsonBuilder.Build();
            var user = new UserBuilder().Build();
            var income = IncomeBuilder.Build(user);

            var useCase = CreateUseCase(user, income.IncomeId, income, incomeFound: true, request.TypeName);

            //Act
            Func<Task> act = async () => await useCase.Execute(income.IncomeId, request);

            //Assert
            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Contains(ResourceMessagesException.INCOME_TYPE_NAME_EXIST, exception.ErrorsMessages);
        }
    }
}
