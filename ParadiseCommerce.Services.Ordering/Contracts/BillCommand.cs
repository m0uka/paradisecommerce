using System;
using System.Collections.Generic;

namespace ParadiseCommerce.Services.Contracts
{
    public class BillCommand
    {
        public Guid CommandId { get; set; }
        public string OrderId { get; set; }
        public BillCustomerInfo CustomerInfo { get; set; }
        
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public List<BillItem> Items { get; set; }
    }
    
    public class BillCustomerInfo
    {
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
    }

    public class BillItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public decimal UnitAmount { get; set; } // The actual price of one item
        public int Quantity { get; set; } = 1;
    }
    
    public class BillPaymentResponse
    {
        public string OrderId { get; set; }
        public string PaymentUrl { get; set; }
    }
    
    public class BillPaymentFinishedResponse
    {
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidAt { get; set; }
    }
}