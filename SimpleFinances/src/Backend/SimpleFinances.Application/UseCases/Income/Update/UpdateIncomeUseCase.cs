using AutoMapper;
using SimpleFinances.Domain.Repositories.Income;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Application.UseCases.Income.Validator;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Exceptions.ExceptionsBase;
using SimpleFinances.Exceptions;

namespace SimpleFinances.Application.UseCases.Income.Update
{
    public class UpdateIncomeUseCase : IUpdateIncomeUseCase
    {
        private readonly IIncomeUpdateOnlyRepository _incomeUpdateOnlyRepository;
        private readonly IIncomeReadOnlyRepository _incomeReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public UpdateIncomeUseCase(IIncomeUpdateOnlyRepository incomeUpdateOnlyRepository, IIncomeReadOnlyRepository incomeReadOnlyRepository, IMapper mapper, IUnityOfWork unityOfWork, ILoggedUser loggedUser)
        {
            _incomeUpdateOnlyRepository = incomeUpdateOnlyRepository;
            _incomeReadOnlyRepository = incomeReadOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task Execute(int idIncome, RequestIncomeJson requestIncome)
        {
            var user = await _loggedUser.User();
            await Validate(requestIncome, user.UserId);

            var income = await _incomeUpdateOnlyRepository.GetIncomeById(user, idIncome);

            if(income is null)
                throw new SimpleFinancesException(ResourceMessagesException.INCOME_NOT_FOUND);

            _mapper.Map(requestIncome, income);

            _incomeUpdateOnlyRepository.Update(income);
            await _unityOfWork.Commit();
        }

        private async Task Validate(RequestIncomeJson requestIncome, int userId)
        {
            var validator = new IncomeValidator();
            var result = validator.Validate(requestIncome);

            var existIncomeTypeName = await _incomeReadOnlyRepository.ExistIncomeTypeName(requestIncome.TypeName, userId);

            if (existIncomeTypeName)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.INCOME_TYPE_NAME_EXIST));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }

}
