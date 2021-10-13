using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using ParadiseCommerce.Services.Billing.Models;
using ParadiseCommerce.Services.Billing.Repositories;
using ParadiseCommerce.Services.Contracts;

namespace ParadiseCommerce.Services.Billing.Services
{
    public class BillService : IBillService
    {
        private readonly IStripePaymentService _stripePaymentService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<BillService> _logger;

        public BillService(IStripePaymentService stripePaymentService, ISendEndpointProvider sendEndpointProvider, IInvoiceRepository invoiceRepository, ILogger<BillService> logger)
        {
            _stripePaymentService = stripePaymentService;
            _sendEndpointProvider = sendEndpointProvider;
            _invoiceRepository = invoiceRepository;
            _logger = logger;
        }

        private decimal CalculateTotal(List<BillItem> items)
        {
            return items.Sum(x => x.UnitAmount * x.Quantity);
        }

        public async Task<Invoice> CreateInvoice(BillCommand command)
        {
            decimal totalCost = CalculateTotal(command.Items);
            
            var invoice = new Invoice
            {
                CustomerId = ObjectId.Parse(command.CustomerInfo.CustomerId),
                OrderId = command.OrderId != null ? ObjectId.Parse(command.OrderId) : null,
                CustomerEmail = command.CustomerInfo.CustomerEmail,
                
                Total = totalCost,
                Currency = command.Currency,
                PaymentMethod = command.PaymentMethod,
                
                BillItems = command.Items,
                
                Status = InvoiceStatus.Unpaid,
                DueAt = DateTime.Now + TimeSpan.FromHours(2),
                
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            
            await _invoiceRepository.Create(invoice);
            _logger.LogInformation($"Created invoice {invoice.Id} for customer {invoice.CustomerId}");
            
            return invoice;
        }

        public async Task<PaymentSchema> CreateInvoicePaymentSchema(Invoice invoice)
        {
            PaymentSchema paymentSchema = null;
            switch (invoice.PaymentMethod)
            {
                case "stripe":
                    paymentSchema = await _stripePaymentService.CreatePaymentLink(invoice,
                        "https://google.com?q=success", "https://google.com?q=fail");
                    break;
                
                default:
                    throw new NotImplementedException($"The payment method {invoice.PaymentMethod} does not exist.");
            }

            _logger.LogInformation($"Created {invoice.PaymentMethod} payment schema for customer {invoice.CustomerId}");
            _logger.LogInformation(paymentSchema.PaymentUrl);
            return paymentSchema;
        }

        public async Task SetInvoicePaid(Invoice invoice, string paymentMethod)
        {
            _logger.LogInformation($"Setting invoice {invoice.Id} as paid!");
            invoice.Status = InvoiceStatus.Paid;
            invoice.PaidAt = DateTime.Now;
            invoice.PaymentMethod = paymentMethod;
            await _invoiceRepository.Update(invoice.Id, invoice);
        }
    }
}