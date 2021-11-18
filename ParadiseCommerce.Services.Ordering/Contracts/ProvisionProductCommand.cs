using System;
using System.Collections.Generic;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Contracts
{
    public class ProvisionProductCommand : IProvisionProductCommand
    {
        public Guid CommandId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public Dictionary<string, string> ProvisionVariables { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public ShippingAddress CustomerAddress { get; set; }
    }
}