using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParadiseCommerce.Services.Storefront.Models;

namespace ParadiseCommerce.Services.Storefront.Repositories
{
    public class StorefrontRepository : IStorefrontRepository
    {
        private readonly IMongoCollection<Models.Storefront> _frontPages;

        public StorefrontRepository(IMongoClient client)
        {
            var database = client.GetDatabase("pcStorefront");
            var collection = database.GetCollection<Models.Storefront>(nameof(Models.Storefront));
            
            _frontPages = collection;
        }
        
        public async Task<ObjectId> Create(Models.Storefront frontPage)
        {
            await _frontPages.InsertOneAsync(frontPage);
            return frontPage.Id;
        }

        public async Task<Models.Storefront> Get()
        {
            var frontPage = await _frontPages.Find(_ => true).FirstOrDefaultAsync();
            return frontPage;
        }

        public async Task<bool> Update(Models.Storefront storefront)
        {
            var update = Builders<Models.Storefront>.Update
                .Set(x => x.StoreName, storefront.StoreName)
                .Set(x => x.StoreLogo, storefront.StoreLogo)
                .Set(x => x.FrontPage, storefront.FrontPage);

            await _frontPages.FindOneAndUpdateAsync(x => true, update);
            return true;
        }

        public Task<bool> Delete(ObjectId objectId)
        {
            throw new System.NotImplementedException();
        }
    }
}