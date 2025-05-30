using OrderManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class Order
    {
        // Default fields
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public int Postcode { get; private set; }

        // One-to-many relationship
        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

        // Computed property for VO immutability
        public ShippingAddress ShippingAddress => new ShippingAddress(Country, City, Street, Postcode);

        // Private constructor that excludes creation of empty object in the database (for EF Core)
        private Order() { } 

        public Order(Guid customerId, ShippingAddress address)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            Postcode = address.Postcode;

            Status = "Created";
            TotalAmount = 0m;
        }

        public void AddOrderItem()
        {
            
        }

        public void ChangeAddress(ShippingAddress address)
        {
            Country = address.Country;
            City = address.City;
            Street = address.Street;
            Postcode = address.Postcode;
        }

        private decimal CalculateTotal() => OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
    }
}
