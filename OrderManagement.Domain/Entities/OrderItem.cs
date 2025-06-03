using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        // navigation
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; } = null!;

        // Prevent the EF Core database from creating an empty entity
        private OrderItem() { }

        /* 
         * If we add an Order to the database explicitly via Add(), we add an OrderItem by changing 
         * the navigation property of the Order (ICollection). If we generate the Id for the OrderItem 
         * ourselves, EF Core will throw an exception when trying to change an EXISTING record 
         * (UPDATE will occur instead of INSERT). That's why there is no explicit assignment of the Id 
         * field in the OrderItem constructor, EF Core will do it for us 
         */
        public OrderItem(Guid orderId, Order order, Guid productId, int qty, decimal unitPrice)
        {
            if (orderId == Guid.Empty)
                throw new DomainException($"{nameof(orderId)} cannot be empty");

            if (productId == Guid.Empty)
                throw new DomainException($"{nameof(productId)} cannot be empty");

            if (qty <= 0 || unitPrice <= 0) 
                throw new DomainException($"{nameof(qty)} or {nameof(unitPrice)} cannot be less than zero");
            
            // Default fields
            ProductId = productId;
            Quantity = qty;
            UnitPrice = unitPrice;

            // Navigation fields
            OrderId = orderId;
            Order = order;
        }
    }
}
