using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Income;
using SimpleFinances.Application.UseCases.Income.Delete;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Income.Delete
{
    public class DeleteIncomeUseCaseTest
    {
        private static DeleteIncomeUseCase CreateUseCase(User user, SimpleFinances.Domain.Entities.Income? income = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var readRepository = new IncomeReadOnlyRepositoryBuilder().GetIncomeById(user, income).Build();
            var writeRepository = IncomeWriteOnlyRepositoryBuilder.Build();
            var unityofWork = UnityOfWorkBuilder.Build();

            return new DeleteIncomeUseCase(writeRepository, readRepository, unityofWork, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var user = new UserBuilder().Build();   
            var income = IncomeBuilder.Build(user);   

            var useCase = CreateUseCase(user, income);

            //Act
            Func<Task> act = async () => await useCase.Execute(income.IncomeId);
            await act();
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
