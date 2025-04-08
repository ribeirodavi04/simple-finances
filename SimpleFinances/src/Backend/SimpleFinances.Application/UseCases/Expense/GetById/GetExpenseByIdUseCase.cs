using AutoMapper;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Domain.Repositories.Expense;

namespace SimpleFinances.Application.UseCases.Expense.GetById
{
    public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
    {
        private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetExpenseByIdUseCase(IExpenseReadOnlyRepository expenseReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
        {
            _expenseReadOnlyRepository = expenseReadOnlyRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseExpenseJson> Execute(int idExpense)
        {
            var user = await _loggedUser.User();

            var expense = await _expenseReadOnlyRepository.GetExpenseById(user, idExpense);

            if (expense is null)
                throw new SimpleFinancesException(ResourceMessagesException.EXPENSE_NOT_FOUND);

            return _mapper.Map<ResponseExpenseJson>(expense);
        }
    }
}
