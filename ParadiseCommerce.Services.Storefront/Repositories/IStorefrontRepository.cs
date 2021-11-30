using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using MongoDB.Bson;
using ParadiseCommerce.Services.Storefront.Models;

namespace ParadiseCommerce.Services.Storefront.Repositories
{
    public interface IStorefrontRepository
    {
        // Create
        Task<ObjectId> Create(Models.Storefront frontPage);
        
        // Read
        Task<Models.Storefront> Get();

        // Update
        Task<bool> Update(Models.Storefront frontPage);
        
        // Delete
        Task<bool> Delete(ObjectId objectId);
    }
}