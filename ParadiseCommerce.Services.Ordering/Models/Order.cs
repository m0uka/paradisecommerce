using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseCommerce.Services.Ordering.Models
{
    /// <summary>
    /// Used for physical and digital products, services for first activation (no recurring payments)
    /// </summary>
    public class Order
    {
        public ObjectId Id { get; set; }
        
        public ObjectId UserId { get; set; }
        public List<OrderItem> Products { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public OrderStatus Status { get; set; }
        
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        
        public string UsedPromo { get; set; }
        
        public ShippingAddress ShippingAddress { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime ActivatedAt { get; set; }
    }

    /// <summary>
    /// Individual products in an order
    /// </summary>
    public class OrderItem
    {
        [Required]
        public string ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        
        [BsonRepresentation(BsonType.String)]
        public RecurringType? RecurringType { get; set; }
        public Dictionary<string, string> ProductOptions { get; set; }
    }
}