using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Communication.Requests
{
    public class RequestIncomeJson
    {
        public string TypeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsRecurring { get; set; }
    }
}
