using AutoMapper;
using SimpleFinances.Application.Services.Cryptography;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Repositories.User;
using SimpleFinances.Domain.Security.Tokens;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository userWriteOnlyRepository, 
            IUserReadOnlyRepository userReadOnlyRepository,
            IMapper mapper,
            PasswordEncripter passwordEncripter,
            IUnityOfWork unityOfWork,
            IAccessTokenGenerator accessTokenGenerator)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unityOfWork = unityOfWork;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson requestUser)
        {
            await Validate(requestUser);

            var user = _mapper.Map<Domain.Entities.User>(requestUser);
            user.Password = _passwordEncripter.Encrypt(requestUser.Password);
            user.UserGuid = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);
            await _unityOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.GenerateToken(user.UserGuid)
                }
            };
        }


        private async Task Validate(RequestRegisterUserJson requestUser)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(requestUser);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(requestUser.Email);
            if (emailExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var errorMessages= result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

        }
    }
}
