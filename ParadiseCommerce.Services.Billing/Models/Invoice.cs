using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ParadiseCommerce.Services.Contracts;

namespace ParadiseCommerce.Services.Billing.Models
{
    public class Invoice
    {
        public ObjectId Id { get; set; }
        public ObjectId CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        
        public ObjectId? OrderId { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public InvoiceStatus Status { get; set; }
        
        public List<BillItem> BillItems { get; set; }
        
        public decimal Total { get; set; }
        public string Currency { get; set; }
        
        public string PaymentMethod { get; set; }
        public string PaymentLink { get; set; }
        
        public DateTime DueAt { get; set; }
        
        public DateTime? PaidAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}