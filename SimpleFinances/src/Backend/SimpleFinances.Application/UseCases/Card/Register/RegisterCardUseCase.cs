using AutoMapper;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories;
using SimpleFinances.Domain.Repositories.Card;
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

        public RegisterCardUseCase(ICardWriteOnlyRepository cardWriteOnlyRepository, IMapper mapper, IUnityOfWork unityOfWork)
        {
            _cardWriteOnlyRepository = cardWriteOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }

        public async Task<ResponseRegisterCardJson> Execute(RequestRegisterCardJson requestCard)
        {
            var card = _mapper.Map<Domain.Entities.Card>(requestCard);
            card.CardGuid = Guid.NewGuid();
            card.UserId = 2;

            await _cardWriteOnlyRepository.Add(card);
            await _unityOfWork.Commit();

            return new ResponseRegisterCardJson
            {
                Name = card.Name
            };
        }

    }
}
