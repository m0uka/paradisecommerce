using System;
using System.Collections.Generic;

namespace ParadiseCommerce.Contracts
{
    public interface IProvisionProductCommand
    {
        Guid CommandId { get; set; }
        
        string ProductId { get; set; }
        Dictionary<string, string> ProvisionVariables { get; set; }
        
        string CustomerId { get; set; }
        string CustomerEmail { get; set; }
        string CustomerAddress { get; set; }
    }
}