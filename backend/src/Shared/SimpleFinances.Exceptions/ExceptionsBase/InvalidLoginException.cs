using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : SimpleFinancesException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID) {}
    }
}
