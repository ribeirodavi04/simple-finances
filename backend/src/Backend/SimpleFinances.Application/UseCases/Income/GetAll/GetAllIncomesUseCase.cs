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

namespace SimpleFinances.Application.UseCases.Income.GetAll
{
    public class GetAllIncomesUseCase : IGetAllIncomesUseCase
    {
        private readonly IIncomeReadOnlyRepository _incomeReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetAllIncomesUseCase(IIncomeReadOnlyRepository incomeReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
        {
            _incomeReadOnlyRepository = incomeReadOnlyRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<IList<ResponseIncomeJson>> Execute()
        {
            var user = await _loggedUser.User();

            var incomes = await _incomeReadOnlyRepository.GetAllIncomes(user);

            if (incomes is null)
                throw new SimpleFinancesException(ResourceMessagesException.INCOME_NOT_FOUND);

            return _mapper.Map<IList<ResponseIncomeJson>>(incomes);
        }
    }
}
