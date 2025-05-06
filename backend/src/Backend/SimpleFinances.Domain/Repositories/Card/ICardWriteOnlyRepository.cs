using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Card
{
    public interface ICardWriteOnlyRepository
    {
        public Task Add(Domain.Entities.Card card);
        public Task Delete(int idCard);
    }
}
