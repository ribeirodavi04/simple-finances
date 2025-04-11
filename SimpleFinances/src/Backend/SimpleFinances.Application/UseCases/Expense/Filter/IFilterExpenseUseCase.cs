using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Expense.Filter
{
    public interface IFilterExpenseUseCase
    {
        public Task<IList<ResponseExpenseJson>> Execute(RequestFilterExpenseJson requestFilter);
    }
}
