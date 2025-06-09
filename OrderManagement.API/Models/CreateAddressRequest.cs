using OrderManagement.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class CreateAddressRequest
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        [Required(ErrorMessage = "PostCode field is required")]
        [Range(1, 999999, ErrorMessage = "PostCode must be a number between 1 and 999999")]
        public int PostCode { get; set; }
    }
}
