using System.Collections.Generic;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ProductImages
    {
        public string Listing { get; set; }
        public string ListingSmall { get; set; }
        public string Bigspot { get; set; }
        
        public List<string> Carousel { get; set; }
    }
}