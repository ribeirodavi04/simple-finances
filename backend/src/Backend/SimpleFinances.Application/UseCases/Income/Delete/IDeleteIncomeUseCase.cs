using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Income.Delete
{
    public interface IDeleteIncomeUseCase
    {
        public Task Execute(int idIncome);
    }
}
