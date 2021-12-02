using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public interface IProductRepository
    {
        // Create
        Task<string> Create(Product product);
        
        // Read
        Task<Product> Get(string objectId);
        Task<IEnumerable<Product>> Get();
        Task<IEnumerable<Product>> GetByGroup(string groupId);
        
        // Update
        Task<bool> Update(string objectId, Product product);
        
        // Delete
        Task<bool> Delete(string objectId);
    }
}