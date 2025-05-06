using AutoMapper;
using SimpleFinances.Domain.Repositories.Income;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Domain.Repositories.Expense;
using SimpleFinances.Application.UseCases.Income.Validator;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;
using FluentValidation;
using SimpleFinances.Application.UseCases.Expense.Validator;

namespace SimpleFinances.Application.UseCases.Expense.Register
{
    public class RegisterExpenseUseCase : IRegisterExpenseUseCase
    {
        private readonly IExpenseWriteOnlyRepository _expenseWriteOnlyRepository;
        private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public RegisterExpenseUseCase(IExpenseWriteOnlyRepository expenseWriteOnlyRepository, IExpenseReadOnlyRepository expenseReadOnlyRepository, IMapper mapper, IUnityOfWork unityOfWork, ILoggedUser loggedUser)
        {
            _expenseWriteOnlyRepository = expenseWriteOnlyRepository;
            _expenseReadOnlyRepository = expenseReadOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseExpenseJson> Execute(RequestExpenseJson requestExpense)
        {
            var user = await _loggedUser.User();

            await Validate(requestExpense, user.UserId);

            var expense = _mapper.Map<Domain.Entities.Expense>(requestExpense);
            expense.UserId = user.UserId;

            await _expenseWriteOnlyRepository.Add(expense);
            await _unityOfWork.Commit();

            return _mapper.Map<ResponseExpenseJson>(expense);
        }

        private async Task Validate(RequestExpenseJson requestExpense, int userId)
        {
            var validator = new ExpenseValidator();
            var result = validator.Validate(requestExpense);

            //var existExpenseTypeName = await _expenseReadOnlyRepository.ExistIncomeTypeName(requestIncome.TypeName, userId);

            //if (existIncomeTypeName)
            //    result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.INCOME_TYPE_NAME_EXIST));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
