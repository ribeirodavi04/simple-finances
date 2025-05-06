using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories.Income;
using SimpleFinances.Application.UseCases.Income.GetById;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Income.GetById
{
    public class GetIncomeByIdUseCaseTest
    {
        private static GetIncomeByIdUseCase CreateUseCase(User user, SimpleFinances.Domain.Entities.Income? income = null)
        {
            var readRepository = new IncomeReadOnlyRepositoryBuilder().GetIncomeById(user, income).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);


            return new GetIncomeByIdUseCase(readRepository, mapper, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var user = new UserBuilder().Build();
            var income = IncomeBuilder.Build(user);

            var useCase = CreateUseCase(user, income);

            //Act
            var result = await useCase.Execute(income.IncomeId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(income.TypeName, result.TypeName);
            Assert.Equal(income.Amount, result.Amount);
            Assert.Equal(income.DateReceived, result.DateReceived);
            Assert.Equal(income.IsRecurring, result.IsRecurring);
        }

        [Fact]
        public async Task Error_Income_Not_Found()
        {
            //Arrange
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user);

            //Act
            Func<Task> act = async () => await useCase.Execute(1);

            //Assert
            var exception = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.INCOME_NOT_FOUND, exception.Message);
        }
    }
}
