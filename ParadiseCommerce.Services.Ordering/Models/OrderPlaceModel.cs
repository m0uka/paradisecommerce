using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class OrderPlaceModel
    {
        [Required]
        public List<OrderItem> ProductInfos { get; set; }
        
        [Required]
        public string Currency { get; set; }
        
        [Required]
        public string PaymentMethod { get; set; }
        
        public ShippingAddress ShippingAddress { get; set; }
        public string PromoCode { get; set; }
    }
    
}