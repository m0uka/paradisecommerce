using System.ComponentModel.DataAnnotations;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ShippingAddress
    {
        [Required]
        public string State { get; set; }
        
        [Required]
        public string StreetAddress { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string ZipCode { get; set; }
        
        [Required]
        public string Country { get; set; }
    }
}