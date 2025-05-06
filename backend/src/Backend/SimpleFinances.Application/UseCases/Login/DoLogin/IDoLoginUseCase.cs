using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson requestLogin);
    }
}
