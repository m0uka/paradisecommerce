using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ConfigurableOption
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId GroupId { get; set; }
        
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        
        public SortedList<int, ConfigurableOptionValue> Values { get; set; } // key is sortOrder, value is optionvalue
    }
}