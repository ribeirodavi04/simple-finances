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
using SimpleFinances.Domain.Repositories.Income;

namespace SimpleFinances.Application.UseCases.Income.GetById
{
    public class GetIncomeByIdUseCase : IGetIncomeByIdUseCase
    {
        private readonly IIncomeReadOnlyRepository _incomeReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetIncomeByIdUseCase(IIncomeReadOnlyRepository incomeReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
        {
            _incomeReadOnlyRepository = incomeReadOnlyRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseIncomeJson> Execute(int idIncome)
        {
            var user = await _loggedUser.User();

            var income = await _incomeReadOnlyRepository.GetIncomeById(user, idIncome);

            if (income is null)
                throw new SimpleFinancesException(ResourceMessagesException.INCOME_NOT_FOUND);

            return _mapper.Map<ResponseIncomeJson>(income);
        }
    }
}
