using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Exceptions
{
    public class OrderIdNotFoundException : Exception
    {
        /// <summary>
        /// Exception class for invalid Ids
        /// </summary>
        public Guid Value { get; }
        public OrderIdNotFoundException(string message, Guid id) : base(message)
        {
            Value = id;
        }
        public OrderIdNotFoundException(string message, Guid id, Exception innerException) : base(message, innerException)
        {
            Value = id;
        }
    }
}
