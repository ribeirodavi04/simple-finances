using SimpleFinances.Domain.Repositories.User;
using SimpleFinances.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.DataAccess
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly SimpleFinancesDbContext _dbContext;

        public UnityOfWork(SimpleFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();    
        }
    }
}
