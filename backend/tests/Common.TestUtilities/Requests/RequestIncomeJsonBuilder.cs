using Bogus;
using SimpleFinances.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Requests
{
    public class RequestIncomeJsonBuilder
    {
        public static RequestIncomeJson Build()
        {
            return new Faker<RequestIncomeJson>()
                .RuleFor(income => income.TypeName, f => f.Company.CompanyName())
                .RuleFor(income => income.Amount, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(income => income.DateReceived, f => f.Date.Future())
                .RuleFor(income => income.IsRecurring, f => f.Random.Bool());
        }
    }
}
