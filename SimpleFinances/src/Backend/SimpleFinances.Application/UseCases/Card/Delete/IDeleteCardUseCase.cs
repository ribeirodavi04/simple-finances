using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.UseCases.Card.Delete
{
    public interface IDeleteCardUseCase
    {
        public Task Execute(int idCard);
    }
}
