using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Income.GetAll
{
    public interface IGetAllIncomesUseCase
    {
        public Task<IList<ResponseIncomeJson>> Execute();
    }
}
