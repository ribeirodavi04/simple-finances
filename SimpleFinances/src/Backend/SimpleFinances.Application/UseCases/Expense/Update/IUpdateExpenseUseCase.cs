using SimpleFinances.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Expense.Update
{
    public interface IUpdateExpenseUseCase
    {
        public  Task Execute(int idExpense, RequestExpenseJson requestExpense);
    }
}
