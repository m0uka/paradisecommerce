using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using ParadiseCommerce.Services.Contracts;
using ParadiseCommerce.Services.Ordering.Models;
using ParadiseCommerce.Services.Ordering.Repositories;

namespace ParadiseCommerce.Services.Ordering.Consumers
{
    public class BillPaymentFinishedConsumer : IConsumer<BillPaymentFinishedResponse>
    {
        private readonly ILogger<BillPaymentFinishedResponse> _logger;
        private readonly IOrderRepository _orderRepository;
        
        public BillPaymentFinishedConsumer(ILogger<BillPaymentFinishedResponse> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }
        
        public async Task Consume(ConsumeContext<BillPaymentFinishedResponse> context)
        {
            _logger.LogInformation($"Received bill payment finished response, order id: {context.Message.OrderId}");

            var order = await _orderRepository.Get(ObjectId.Parse(context.Message.OrderId));
            if (order == null)
            {
                _logger.LogWarning($"Received a bill payment, but it has an invalid OrderId! Ignoring!");
                return;
            }

            order.Status = OrderStatus.Active;
            order.PaymentMethod = context.Message.PaymentMethod;
            order.ActivatedAt = context.Message.PaidAt;
            
            await _orderRepository.Update(order.Id, order);
            
            _logger.LogInformation($"Order {order.Id} has been paid for with {order.PaymentMethod}, and is now active!");

            // We can call the provisioning service from here!
        }
    }
}