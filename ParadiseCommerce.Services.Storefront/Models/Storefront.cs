using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace ParadiseCommerce.Services.Storefront.Models
{
    public class Storefront
    {
        public ObjectId Id { get; set; }
        
        [Required]
        public string StoreName { get; set; }
        public string StoreLogo { get; set; }
        public FrontPage FrontPage { get; set; }
        
        public List<string> NavbarCategories { get; set; }
    }
}