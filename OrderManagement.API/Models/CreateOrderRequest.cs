using OrderManagement.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class CreateAddressRequest
    {
        [Required(ErrorMessage = "Country field is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City field is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street field is required")]
        public string Street { get; set; }

        [Range(1, 999999, ErrorMessage = "PostCode must be a number between 1 and 999999")]
        public int PostCode { get; set; }
    }
}
