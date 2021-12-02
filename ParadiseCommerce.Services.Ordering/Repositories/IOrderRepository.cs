using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public interface IOrderRepository
    {
        // Create
        Task<string> Create(Order order);
        
        // Read
        Task<Order> Get(string objectId);
        Task<IEnumerable<Order>> Get();
        Task<IEnumerable<Order>> GetByUser(string userId);
        
        // Update
        Task<bool> Update(string objectId, Order order);
        
        // Delete
        Task<bool> Delete(string objectId);
    }
}