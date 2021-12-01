using System.Collections.Generic;
using MongoDB.Bson;

namespace ParadiseCommerce.Services.Storefront.Models
{
    public class FrontPage
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageUrl { get; set; }
        
        public string MainButtonText { get; set; }
        public string MainButtonLocation { get; set; }
        
        public List<string> FeaturedCategories { get; set; }
    }
}