using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public interface ICategoryRepository
    {
        // Create
        Task<string> Create(ProductCategory product);
        
        // Read
        Task<ProductCategory> Get(string objectId);
        Task<IEnumerable<ProductCategory>> Get();

        // Update
        Task<bool> Update(string objectId, ProductCategory product);
        
        // Delete
        Task<bool> Delete(string objectId);
    }
}