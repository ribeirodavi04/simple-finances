using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Income
{
    public interface IIncomeUpdateOnlyRepository
    {
        Task<Entities.Income?> GetIncomeById(Entities.User user, int idIncome);
        void Update(Entities.Income income);
    }
}
