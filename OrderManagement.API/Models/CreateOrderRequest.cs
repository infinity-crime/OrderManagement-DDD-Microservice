using OrderManagement.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }

        public ShippingAddress ShippingAddress { get; set; }
    }
}
