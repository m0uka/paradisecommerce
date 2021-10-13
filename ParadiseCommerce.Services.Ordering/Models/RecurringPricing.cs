using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class RecurringPricing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ProductId { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public string Currency { get; set; }
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal SetupFee { get; set; }
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Monthly { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Quarterly { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal SemiAnnually { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Annually { get; set; }
    }
}