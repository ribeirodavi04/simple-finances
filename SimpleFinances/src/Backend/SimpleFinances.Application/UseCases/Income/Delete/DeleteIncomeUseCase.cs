using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Domain.Repositories.Income;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;

namespace SimpleFinances.Application.UseCases.Income.Delete
{
    public class DeleteIncomeUseCase : IDeleteIncomeUseCase
    {
        private readonly IIncomeWriteOnlyRepository _incomeWriteOnlyRepository;
        private readonly IIncomeReadOnlyRepository _incomeReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly ILoggedUser _loggedUser;

        public DeleteIncomeUseCase(
            IIncomeWriteOnlyRepository incomeWriteOnlyRepository,
            IIncomeReadOnlyRepository incomeReadOnlyRepository,
            IUnityOfWork unityOfWork,
            ILoggedUser loggedUser)
        {
            _incomeWriteOnlyRepository = incomeWriteOnlyRepository;
            _incomeReadOnlyRepository = incomeReadOnlyRepository;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task Execute(int idIncome)
        {
            var user = await _loggedUser.User();

            var income = await _incomeReadOnlyRepository.GetIncomeById(user, idIncome);

            if (income is null)
                throw new SimpleFinancesException(ResourceMessagesException.INCOME_NOT_FOUND);

            await _incomeWriteOnlyRepository.Delete(idIncome);
            await _unityOfWork.Commit();
        }
    }
}
