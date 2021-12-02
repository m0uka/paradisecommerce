using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public interface ICategoryRepository
    {
        // Create
        Task<ObjectId> Create(ProductCategory product);
        
        // Read
        Task<ProductCategory> Get(ObjectId objectId);
        Task<IEnumerable<ProductCategory>> Get();

        // Update
        Task<bool> Update(ObjectId objectId, ProductCategory product);
        
        // Delete
        Task<bool> Delete(ObjectId objectId);
    }
}