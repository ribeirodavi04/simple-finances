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
using SimpleFinances.Communication.Requests;
using SimpleFinances.Application.UseCases.Expense.Validator;

namespace SimpleFinances.Application.UseCases.Expense.Filter
{
    public class FilterExpenseUseCase : IFilterExpenseUseCase
    {
        private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public FilterExpenseUseCase(IExpenseReadOnlyRepository expenseReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
        {
            _expenseReadOnlyRepository = expenseReadOnlyRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<IList<ResponseExpenseJson>> Execute(RequestFilterExpenseJson requestFilter)
        {
            Validate(requestFilter); 
            
            var user = await _loggedUser.User();

            var filters = new Domain.DTOs.FilterExpenseDTO
            {
                Name = requestFilter.Name,
                CardsIds = requestFilter.CardsIds,
                StartDate = requestFilter.StartDate,
                EndDate = requestFilter.EndDate,
                ValueFrom = requestFilter.ValueFrom,
                ValueTo = requestFilter.ValueTo,
                IsRecurring = requestFilter.IsRecurring,
            };

            var expenses = await _expenseReadOnlyRepository.Filter(user, filters);

            if (expenses is null)
                throw new SimpleFinancesException(ResourceMessagesException.EXPENSE_NOT_FOUND);

            return _mapper.Map<IList<ResponseExpenseJson>>(expenses);
        }

        public static void Validate(RequestFilterExpenseJson requestFilter)
        {
            var validator = new FilterExpenseValidator();

            var result = validator.Validate(requestFilter);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
