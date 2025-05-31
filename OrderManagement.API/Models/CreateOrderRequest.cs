using OrderManagement.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class CreateOrderRequest
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "ShippingAddress is required")]
        public ShippingAddress ShippingAddress { get; set; }
    }
}
