using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class CreateOrderItemRequest
    {
        [Required(ErrorMessage = "ProductId field is required")]
        public Guid ProductId { get; set; }

        [Range(1, 100, ErrorMessage = "Quantity must be a number between 1 and 999999")]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
