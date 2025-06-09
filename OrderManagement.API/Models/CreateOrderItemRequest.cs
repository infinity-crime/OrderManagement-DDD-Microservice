using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class CreateOrderItemRequest
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity field is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be a number between 1 and 999999")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "UnitPrice field is required")]
        [Range(0.01, 1000000.00, ErrorMessage = "UnitPrice must be a number between 1 and 999999")]
        public decimal UnitPrice { get; set; }
    }
}
