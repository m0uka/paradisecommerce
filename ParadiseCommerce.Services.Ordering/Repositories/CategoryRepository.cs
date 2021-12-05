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
        
        public async Task<string> Create(ProductCategory category)
        {
            await _categories.InsertOneAsync(category);
            return category.Id;
        }

        public async Task<ProductCategory> Get(string objectId)
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.Id, objectId.ToString());
            var product = await _categories.Find(filter).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<ProductCategory>> Get()
        {
            return await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<bool> Update(string objectId, ProductCategory category)
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<ProductCategory>.Update
                .Set(x => x.Name, category.Name)
                .Set(x => x.Description, category.Description)
                .Set(x => x.ShowInNavbar, category.ShowInNavbar)
                .Set(x => x.Slug, category.Slug)
                .Set(x => x.BigspotImage, category.BigspotImage);

            var result = await _categories.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(string objectId)
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.Id, objectId);
            var result = await _categories.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}