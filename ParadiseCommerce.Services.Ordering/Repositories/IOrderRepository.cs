using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public interface IOrderRepository
    {
        // Create
        Task<ObjectId> Create(Order order);
        
        // Read
        Task<Order> Get(ObjectId objectId);
        Task<IEnumerable<Order>> Get();
        Task<IEnumerable<Order>> GetByUser(ObjectId userId);
        
        // Update
        Task<bool> Update(ObjectId objectId, Order order);
        
        // Delete
        Task<bool> Delete(ObjectId objectId);
    }
}