using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.Register
{
    public interface IRegisterCardUseCase
    {
        public Task<ResponseRegisteredCardJson> Execute(RequestCardJson requestCard);
    }
}
