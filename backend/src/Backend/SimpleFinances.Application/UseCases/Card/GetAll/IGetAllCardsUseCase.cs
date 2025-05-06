using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.GetAll
{
    public interface IGetAllCardsUseCase
    {
        public Task<IList<ResponseCardJson>> Execute();
    }
}
