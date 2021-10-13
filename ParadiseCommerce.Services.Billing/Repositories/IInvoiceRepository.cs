using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Billing.Models;

namespace ParadiseCommerce.Services.Billing.Repositories
{
    public interface IInvoiceRepository
    {
        // Create
        Task<ObjectId> Create(Invoice invoice);
        
        // Read
        Task<Invoice> Get(ObjectId objectId);
        Task<IEnumerable<Invoice>> Get();
        Task<IEnumerable<Invoice>> GetByUser(ObjectId userId);
        
        // Update
        Task<bool> Update(ObjectId objectId, Invoice order);
        
        // Delete
        Task<bool> Delete(ObjectId objectId);
    }
}