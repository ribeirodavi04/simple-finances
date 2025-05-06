using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Domain.Repositories.Expense;

namespace SimpleFinances.Application.UseCases.Expense.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IExpenseWriteOnlyRepository _expenseWriteOnlyRepository;
        private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly ILoggedUser _loggedUser;

        public DeleteExpenseUseCase(
            IExpenseWriteOnlyRepository expenseWriteOnlyRepository,
            IExpenseReadOnlyRepository expenseReadOnlyRepository,
            IUnityOfWork unityOfWork,
            ILoggedUser loggedUser)
        {
            _expenseWriteOnlyRepository = expenseWriteOnlyRepository;
            _expenseReadOnlyRepository = expenseReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task Execute(int idExpense)
        {
            var user = await _loggedUser.User();

            var expense = await _expenseReadOnlyRepository.GetExpenseById(user, idExpense);

            if (expense is null)
                throw new SimpleFinancesException(ResourceMessagesException.EXPENSE_NOT_FOUND);

            await _expenseWriteOnlyRepository.Delete(idExpense);
            await _unityOfWork.Commit();
        }
    }
}
