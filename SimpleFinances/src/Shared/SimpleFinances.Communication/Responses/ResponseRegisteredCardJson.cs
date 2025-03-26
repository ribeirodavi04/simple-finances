using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Communication.Responses
{
    public class ResponseRegisteredCardJson
    {
        public string Name { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Bank { get; set; } = string.Empty;
        public decimal Limit { get; set; }
        public DateTime? CardDueDate { get; set; }
        public DateTime? CardClosingDate { get; set; }
    }
}
