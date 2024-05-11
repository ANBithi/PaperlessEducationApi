using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message)
            : base(message)
        {
        }
    }
}
