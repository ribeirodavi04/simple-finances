using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Income
{
    public interface IIncomeWriteOnlyRepository
    {
        public Task Add(Domain.Entities.Income income);
        public Task Delete(int idIncome);
    }
}
