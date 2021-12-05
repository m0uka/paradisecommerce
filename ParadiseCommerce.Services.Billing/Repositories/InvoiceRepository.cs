using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParadiseCommerce.Services.Billing.Models;

namespace ParadiseCommerce.Services.Billing.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IMongoCollection<Invoice> _invoices;

        public InvoiceRepository(IMongoClient client)
        {
            var database = client.GetDatabase("pcBilling");
            var collection = database.GetCollection<Invoice>(nameof(Invoice));
            
            _invoices = collection;
            
        }
        public async Task<string> Create(Invoice invoice)
        {
            await _invoices.InsertOneAsync(invoice);
            return invoice.Id;
        }

        public async Task<Invoice> Get(string objectId)
        {
            var filter = Builders<Invoice>.Filter.Eq(c => c.Id, objectId);
            var product = await _invoices.Find(filter).FirstOrDefaultAsync();

            return product;
        }

        public Task<IEnumerable<Invoice>> Get()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetByUser(string userId)
        {
            var filter = Builders<Invoice>.Filter.Eq(c => c.CustomerId, userId);
            var invoices = await _invoices.Find(filter).ToListAsync();

            return invoices;
        }
        
        public async Task<Invoice> GetByOrder(string orderId)
        {
            var filter = Builders<Invoice>.Filter.Eq(c => c.OrderId, orderId);
            var invoice = await _invoices.Find(filter).FirstOrDefaultAsync();

            return invoice;
        }

        public async Task<bool> Update(string objectId, Invoice invoice)
        {
            var filter = Builders<Invoice>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<Invoice>.Update
                .Set(x => x.Status, invoice.Status)
                .Set(x => x.PaidAt, invoice.PaidAt)
                .Set(x => x.CancelledAt, invoice.CancelledAt)
                .Set(x => x.DueAt, invoice.DueAt)
                .Set(x => x.PaymentMethod, invoice.PaymentMethod)
                .Set(x => x.PaymentLink, invoice.PaymentLink)
                .Set(x => x.UpdatedAt, DateTime.Now);
                
            var result = await _invoices.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public Task<bool> Delete(string objectId)
        {
            throw new System.NotImplementedException();
        }
    }
}