using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Entities
{
    public class Income
    {
        public int IncomeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsRecurring { get; set; }
        public int UserId { get; set; }

    }
}
