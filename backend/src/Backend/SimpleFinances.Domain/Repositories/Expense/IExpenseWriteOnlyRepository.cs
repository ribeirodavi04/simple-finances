using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Expense
{
    public interface IExpenseWriteOnlyRepository
    {
        public Task Add(Domain.Entities.Expense expense);
        public Task Delete(int idExpense);
    }
}
