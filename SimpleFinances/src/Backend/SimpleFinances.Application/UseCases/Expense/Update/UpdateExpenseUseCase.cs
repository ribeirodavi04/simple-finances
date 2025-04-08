using AutoMapper;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Domain.Repositories.Expense;
using SimpleFinances.Application.UseCases.Card.Validator;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using SimpleFinances.Application.UseCases.Expense.Validator;

namespace SimpleFinances.Application.UseCases.Expense.Update
{
    public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IExpenseUpdateOnlyRepository _expenseUpdateOnlyRepository;
        private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public UpdateExpenseUseCase(
            IExpenseUpdateOnlyRepository expenseUpdateeOnlyRepository,
            IExpenseReadOnlyRepository expenseReadOnlyRepository,
            IUnityOfWork unityOfWork,
            IMapper mapper,
            ILoggedUser loggedUser)
        {
            _expenseUpdateOnlyRepository = expenseUpdateeOnlyRepository;
            _expenseReadOnlyRepository = expenseReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task Execute(int idExpense, RequestExpenseJson requestExpense)
        {
            var loggedUser = await _loggedUser.User();
            await Validate(requestExpense, loggedUser.UserId);

            var expense = await _expenseUpdateOnlyRepository.GetExpenseById(loggedUser, idExpense);

            if (expense is null)
                throw new SimpleFinancesException(ResourceMessagesException.EXPENSE_NOT_FOUND);

            _mapper.Map(requestExpense, expense);

            _expenseUpdateOnlyRepository.Update(expense);
            await _unityOfWork.Commit();
        }

        private async Task Validate(RequestExpenseJson requestExpense, int userId)
        {
            var result = new ExpenseValidator().Validate(requestExpense);

            //var cardNameExist = await _expenReadOnlyRepository.ExistCardName(requestCard.Name, userId);

            //if (cardNameExist)
            //    result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.CARD_NAME_ALREADY_EXIST));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

        }
    }
}
