using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using ParadiseCommerce.Services.Billing.Services;
using ParadiseCommerce.Services.Contracts;

namespace ParadiseCommerce.Services.Billing.Consumers
{
    public class BillConsumer : IConsumer<BillCommand>
    {
        private readonly IBillService _billService;
        private readonly ILogger<BillConsumer> _logger;
        
        public BillConsumer(IBillService billService, ILogger<BillConsumer> logger)
        {
            _billService = billService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BillCommand> context)
        {
            _logger.LogInformation($"Received bill command, order id: {context.Message.OrderId}");

            var invoice = await _billService.CreateInvoice(context.Message);
            var paymentSchema = await _billService.CreateInvoicePaymentSchema(invoice);

            var sendEndpoint = await context.GetSendEndpoint(new Uri("queue:billing-service-payment-finish"));
            await sendEndpoint.Send(new BillPaymentResponse
            {
                OrderId = context.Message.OrderId,
                PaymentUrl = paymentSchema.PaymentUrl
            });
        }

        
    }
}