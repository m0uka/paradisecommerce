using MongoDB.Bson;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ProductCategory
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public string BigspotImage { get; set; }
    }
}