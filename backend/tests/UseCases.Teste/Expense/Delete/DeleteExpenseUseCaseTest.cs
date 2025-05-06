using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Repositories.Expense;
using SimpleFinances.Application.UseCases.Expense.Delete;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Expense.Delete
{
    public class DeleteExpenseUseCaseTest
    {
        public DeleteExpenseUseCase CreateUseCase(User user, SimpleFinances.Domain.Entities.Expense? expense = null)
        {
            var writeRepository = ExpenseWriteOnlyRepositoryBuilder.Build();
            var readRepository = new ExpenseReadOnlyRepositoryBuilder().GetExpenseById(user, expense).Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);   

            return new DeleteExpenseUseCase(writeRepository, readRepository, unityOfWork, loggedUser);
        }

        [Fact]
        public async Task Success()
        {
            //Arrange
            var user = new UserBuilder().Build();
            var card = CardBuilder.Build(user);
            var expense = ExpenseBuilder.Build(user, card);

            var useCase = CreateUseCase(user, expense);

            //Act
            Func<Task> act = async () => await useCase.Execute(expense.ExpenseId);
            await act();
        }

        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            //Arrange
            var user = new UserBuilder().Build();           

            var useCase = CreateUseCase(user);

            //Act
            Func<Task> act = async () => await useCase.Execute(1);

            //Assert
            var expection = await Assert.ThrowsAsync<SimpleFinancesException>(act);
            Assert.Equal(ResourceMessagesException.EXPENSE_NOT_FOUND, expection.Message);
        }
    }
}
