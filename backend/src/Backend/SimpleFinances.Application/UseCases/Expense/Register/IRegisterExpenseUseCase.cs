using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Expense.Register
{
    public interface IRegisterExpenseUseCase
    {
        public Task<ResponseExpenseJson> Execute(RequestExpenseJson requestExpense);
    }
}
