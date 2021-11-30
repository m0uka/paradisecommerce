using MongoDB.Bson;

namespace ParadiseCommerce.Services.Storefront.Models
{
    public class Storefront
    {
        public ObjectId Id { get; set; }
        
        public string StoreName { get; set; }
        public FrontPage FrontPage { get; set; }
    }
}