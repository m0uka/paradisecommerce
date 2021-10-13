using System;
using System.Collections.Generic;

namespace ParadiseCommerce.Contracts
{
    public class ProvisionProductCommand : IProvisionProductCommand
    {
        public Guid CommandId { get; set; }
        public string ProductId { get; set; }
        public Dictionary<string, string> ProvisionVariables { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
    }
}