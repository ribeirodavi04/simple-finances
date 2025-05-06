using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Domain.Entities.User user);
    }
}
