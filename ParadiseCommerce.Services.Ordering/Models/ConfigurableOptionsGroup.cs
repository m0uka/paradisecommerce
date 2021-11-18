using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ConfigurableOptionsGroup
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId ProductId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}