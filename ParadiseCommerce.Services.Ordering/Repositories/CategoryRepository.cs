using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<ProductCategory> _categories;

        public CategoryRepository(IMongoClient client)
        {
            var database = client.GetDatabase("pcProducts");
            var collection = database.GetCollection<ProductCategory>(nameof(ProductCategory));
            
            _categories = collection;
        }
        
        public async Task<ObjectId> Create(ProductCategory category)
        {
            await _categories.InsertOneAsync(category);
            return category.Id;
        }

        public async Task<ProductCategory> Get(ObjectId objectId)
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.Id, objectId);
            var product = await _categories.Find(filter).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<ProductCategory>> Get()
        {
            return await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<bool> Update(ObjectId objectId, ProductCategory category)
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<ProductCategory>.Update
                .Set(x => x.Name, category.Name)
                .Set(x => x.BigspotImage, category.BigspotImage);

            var result = await _categories.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.Id, objectId);
            var result = await _categories.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}