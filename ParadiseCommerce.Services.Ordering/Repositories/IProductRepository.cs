using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public interface IProductRepository
    {
        // Create
        Task<ObjectId> Create(Product product);
        
        // Read
        Task<Product> Get(ObjectId objectId);
        Task<IEnumerable<Product>> Get();
        Task<IEnumerable<Product>> GetByGroup(ObjectId groupId);
        
        // Update
        Task<bool> Update(ObjectId objectId, Product product);
        
        // Delete
        Task<bool> Delete(ObjectId objectId);
    }
}