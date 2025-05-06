using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Exceptions.ExceptionsBase
{
    public class SimpleFinancesException : SystemException
    {
        public SimpleFinancesException(string message) : base(message) { }
    }
}
