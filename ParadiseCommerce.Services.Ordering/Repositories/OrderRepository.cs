using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParadiseCommerce.Services.Ordering.Models;

namespace ParadiseCommerce.Services.Ordering.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoClient client)
        {
            var database = client.GetDatabase("pcProducts");
            var collection = database.GetCollection<Order>(nameof(Order));
            
            _orders = collection;
        }
        
        public async Task<string> Create(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order.Id;
        }

        public async Task<Order> Get(string objectId)
        {
            var filter = Builders<Order>.Filter.Eq(c => c.Id, objectId);
            var order = await _orders.Find(filter).FirstOrDefaultAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            var orders = await _orders.Find(_ => true).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetByUser(string userId)
        {
            var filter = Builders<Order>.Filter.Eq(c => c.UserId, userId);
            var orders = await _orders.Find(filter).ToListAsync();

            return orders;
        }

        public async Task<bool> Update(string objectId, Order order)
        {
            var filter = Builders<Order>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<Order>.Update
                .Set(x => x.Status, order.Status)
                .Set(x => x.PaymentMethod, order.PaymentMethod)
                .Set(x => x.ActivatedAt, order.ActivatedAt);

            var result = await _orders.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public Task<bool> Delete(string objectId)
        {
            throw new System.NotImplementedException();
        }
    }
}