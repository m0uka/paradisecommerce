﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public ObjectId GroupId { get; set; }
        
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.String)]
        public ProductType ProductType { get; set; }
        
        public ProductPricing Pricing { get; set; }
        
        public bool IsHidden { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}