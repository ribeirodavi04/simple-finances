using AutoMapper;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFinances.Domain.Repositories.Income;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Application.UseCases.Income.Validator;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;

namespace SimpleFinances.Application.UseCases.Income.Register
{
    public class RegisterIncomeUseCase : IRegisterIncomeUseCase
    {
        private readonly IIncomeWriteOnlyRepository _incomeWriteOnlyRepository;
        private readonly IIncomeReadOnlyRepository _incomeReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public RegisterIncomeUseCase(IIncomeWriteOnlyRepository incomeWriteOnlyRepository, IIncomeReadOnlyRepository incomeReadOnlyRepository, IMapper mapper, IUnityOfWork unityOfWork, ILoggedUser loggedUser)
        {
            _incomeWriteOnlyRepository = incomeWriteOnlyRepository;
            _incomeReadOnlyRepository = incomeReadOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseIncomeJson> Execute(RequestIncomeJson requestIncome)
        {
            var user = await _loggedUser.User();

            await Validate(requestIncome, user.UserId);

            var income = _mapper.Map<Domain.Entities.Income>(requestIncome);
            income.UserId = user.UserId;

            await _incomeWriteOnlyRepository.Add(income);
            await _unityOfWork.Commit();

            return _mapper.Map<ResponseIncomeJson>(income);
        }

        private async Task Validate(RequestIncomeJson requestIncome, int userId)
        {
            var validator = new IncomeValidator();
            var result = validator.Validate(requestIncome);

            var existIncomeTypeName = await _incomeReadOnlyRepository.ExistIncomeTypeName(requestIncome.TypeName, userId);

            if(existIncomeTypeName)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.INCOME_TYPE_NAME_EXIST));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
