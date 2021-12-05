using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Billing.Models;

namespace ParadiseCommerce.Services.Billing.Repositories
{
    public interface IInvoiceRepository
    {
        // Create
        Task<string> Create(Invoice invoice);
        
        // Read
        Task<Invoice> Get(string objectId);
        Task<IEnumerable<Invoice>> Get();
        Task<IEnumerable<Invoice>> GetByUser(string userId);
        Task<Invoice> GetByOrder(string orderId);
        
        // Update
        Task<bool> Update(string objectId, Invoice order);
        
        // Delete
        Task<bool> Delete(string objectId);
    }
}