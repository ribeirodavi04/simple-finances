using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Income
{
    public interface IIncomeReadOnlyRepository
    {
        public Task<IList<Domain.Entities.Income>> GetAllIncomes(Domain.Entities.User user);
        public Task<Domain.Entities.Income?> GetIncomeById(Domain.Entities.User user, int idIncome);
        public Task<bool> ExistIncomeTypeName(string name, int userId);
    }
}
