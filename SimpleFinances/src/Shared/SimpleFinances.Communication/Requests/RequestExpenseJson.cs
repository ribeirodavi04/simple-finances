using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Communication.Requests
{
    public class RequestExpenseJson
    {
        public string Name { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? DateExpense { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? CardId { get; set; }
    }
}
