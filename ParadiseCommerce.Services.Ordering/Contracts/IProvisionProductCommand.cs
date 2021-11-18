using System;
using System.Collections.Generic;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Contracts
{
    public interface IProvisionProductCommand
    {
        Guid CommandId { get; set; }
        
        string ProductId { get; set; }
        int Quantity { get; set; }
        Dictionary<string, string> ProvisionVariables { get; set; }
        
        string CustomerId { get; set; }
        string CustomerEmail { get; set; }
        ShippingAddress CustomerAddress { get; set; }
    }
}