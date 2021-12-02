using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoClient client)
        {
            var database = client.GetDatabase("pcProducts");
            var collection = database.GetCollection<Product>(nameof(Product));
            
            _products = collection;
        }

        public async Task<ObjectId> Create(Product product)
        {
            await _products.InsertOneAsync(product);
            return product.Id;
        }

        public async Task<Product> Get(ObjectId objectId)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, objectId);
            var product = await _products.Find(filter).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _products.Find(_ => true).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetByGroup(ObjectId groupId)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.GroupId, groupId);
            var products = await _products.Find(filter).ToListAsync();

            return products;
        }

        public async Task<bool> Update(ObjectId objectId, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<Product>.Update
                .Set(x => x.GroupId, product.GroupId)
                .Set(x => x.Name, product.Name)
                .Set(x => x.Description, product.Description)
                .Set(x => x.Slug, product.Slug)
                .Set(x => x.IsHidden, product.IsHidden)
                .Set(x => x.UpdatedAt, DateTime.Now);
                
            var result = await _products.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, objectId);
            var result = await _products.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}