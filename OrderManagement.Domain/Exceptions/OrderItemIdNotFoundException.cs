using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Exceptions
{
    public class OrderItemIdNotFoundException : OrderIdNotFoundException
    {
        public OrderItemIdNotFoundException(string message, Guid id) : base(message, id) { }

        public OrderItemIdNotFoundException(string message, Guid id, Exception innerException)
            : base(message, id, innerException) { }

    }
}
