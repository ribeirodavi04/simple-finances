using Bogus;
using SimpleFinances.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Requests
{
    public class RequestCardJsonBuilder
    {
        public static RequestCardJson Build()
        {
            return new Faker<RequestCardJson>()
                .RuleFor(card => card.Name, f => f.Finance.AccountName())
                .RuleFor(card => card.TypeName, f => f.PickRandom(new[] { "Crédito", "Débito" }))
                .RuleFor(card => card.Bank, f => f.Company.CompanyName())
                .RuleFor(card => card.Limit, f => f.Random.Decimal(0, 50000))
                .RuleFor(card => card.CardDueDate, f => f.Date.Future())
                .RuleFor(card => card.CardClosingDate, f => f.Date.Future());
                
        }
    }
}
