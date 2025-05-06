using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : SimpleFinancesException
    {
        public IList<string> ErrorsMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorsMessages) : base(string.Empty) 
        {
            ErrorsMessages = errorsMessages;
        }
    }
}
