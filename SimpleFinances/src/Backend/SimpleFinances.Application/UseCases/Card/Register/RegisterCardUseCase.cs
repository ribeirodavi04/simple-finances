using AutoMapper;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Repositories.Card;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.Register
{
    public class RegisterCardUseCase : IRegisterCardUseCase
    {
        private readonly ICardWriteOnlyRepository _cardWriteOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public RegisterCardUseCase(ICardWriteOnlyRepository cardWriteOnlyRepository, IMapper mapper, IUnityOfWork unityOfWork, ILoggedUser loggedUser)
        {
            _cardWriteOnlyRepository = cardWriteOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseRegisteredCardJson> Execute(RequestRegisterCardJson requestCard)
        {
            var card = _mapper.Map<Domain.Entities.Card>(requestCard);

            var loggedUser = await _loggedUser.User();

            card.CardGuid = Guid.NewGuid();
            card.UserId = loggedUser.UserId;

            await _cardWriteOnlyRepository.Add(card);
            await _unityOfWork.Commit();

            return _mapper.Map<ResponseRegisteredCardJson>(card);
        }

    }
}
