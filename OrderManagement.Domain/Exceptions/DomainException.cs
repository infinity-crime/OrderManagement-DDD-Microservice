using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Exceptions
{
    public class DomainException : Exception
    {
        /// <summary>
        /// Base class for all domain logic (constructors) exceptions
        /// </summary>
        
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
