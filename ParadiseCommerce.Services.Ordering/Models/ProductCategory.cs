using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ProductCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        
        public string Description { get; set; }
        public bool ShowInNavbar { get; set; }
        
        public string BigspotImage { get; set; }
    }
}