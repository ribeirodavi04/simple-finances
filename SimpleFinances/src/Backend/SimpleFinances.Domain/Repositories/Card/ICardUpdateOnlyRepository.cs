using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Repositories.Card
{
    public interface ICardUpdateOnlyRepository
    {
        Task<Entities.Card?> GetCardById(Entities.User user, int idCard);
        void Update(Entities.Card card);
    }
}
