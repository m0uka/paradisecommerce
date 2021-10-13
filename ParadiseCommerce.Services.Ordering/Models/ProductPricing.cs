using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class ProductPricing
    {
        public Dictionary<string, decimal> CurrencyPrices { get; set; }
    }
}