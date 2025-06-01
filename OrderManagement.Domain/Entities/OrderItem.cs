using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private OrderItem() { }

        public OrderItem(Guid orderId, Order order, Guid productId, int qty, decimal unitPrice)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentNullException("Order ID must not be empty",
                    nameof(orderId));

            if (productId == Guid.Empty)
                throw new ArgumentNullException("Product ID must not be empty", 
                    nameof(productId));

            if (qty <= 0) 
                throw new ArgumentOutOfRangeException("The quantity of goods cannot be less than zero", 
                    nameof(qty));

            if(unitPrice <= 0) 
                throw new ArgumentOutOfRangeException("The price of a product cannot be less than zero", 
                    nameof(unitPrice));

            
            OrderId = orderId;
            ProductId = productId;
            Quantity = qty;
            UnitPrice = unitPrice;

            Order = order;
        }
    }
}
