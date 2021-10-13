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
        
        public async Task<ObjectId> Create(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order.Id;
        }

        public Task<Order> Get(ObjectId objectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Order>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetByUser(ObjectId userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(ObjectId objectId, Order order)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(ObjectId objectId)
        {
            throw new System.NotImplementedException();
        }
    }
}