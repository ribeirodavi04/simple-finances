using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Mapper;
using Common.TestUtilities.Repositories.Income;
using SimpleFinances.Application.UseCases.Income.GetAll;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Income.GetAll
{
    public class GetAllIncomesUseCaseTest
    {
        
        private static GetAllIncomesUseCase CreateUseCase(User user, IList<SimpleFinances.Domain.Entities.Income>? incomes = null)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var readRepository = new IncomeReadOnlyRepositoryBuilder().GetAllIncomes(user, incomes).Build();

            return new GetAllIncomesUseCase(readRepository, mapper, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Assert
            var user = new UserBuilder().Build();
            var incomes = IncomeBuilder.Collection(user);

            var useCase = CreateUseCase(user, incomes);

            //Act
            var result = await useCase.Execute();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(incomes.Count, result.Count);
        }

        [Fact]
        public async Task Error_Incomes_Not_Found()
        {
            //Arrange
            var user = new UserBuilder().Build();

            var useCase = CreateUseCase(user, null);

            //Act
            Func<Task> act = async () => await useCase.Execute();

            //Assert
            var exception = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.INCOME_NOT_FOUND, exception.Message);
        }
    }
}
