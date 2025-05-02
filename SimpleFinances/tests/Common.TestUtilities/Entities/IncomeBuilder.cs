using Bogus;
using SimpleFinances.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Entities
{
    public class IncomeBuilder
    {
        public static IList<Income> Collection(User user, uint count = 2)
        {
            var incomes = new List<Income>();

            if(count == 0) count = 1;

            var incomeId = 1;

            for(int i = 0; i < count; i++)
            {
                var fakeIncome = Build(user);
                fakeIncome.IncomeId = incomeId++;

                incomes.Add(fakeIncome);
            }

            return incomes;
        }

        public static Income Build(User user)
        {
            var income = new Faker<Income>()
                .RuleFor(income => income.IncomeId, f => f.IndexFaker)
                .RuleFor(income => income.TypeName, f => f.Company.CompanyName())
                .RuleFor(income => income.Amount, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(income => income.DateReceived, f => f.Date.Future())
                .RuleFor(income => income.IsRecurring, f => f.Random.Bool())
                .RuleFor(income => income.UserId, f => user.UserId);

            return income;
        }
    }
}
