using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using ParadiseCommerce.Services.Billing.Models;
using ParadiseCommerce.Services.Billing.Repositories;
using ParadiseCommerce.Services.Billing.Services;
using Stripe;
using Stripe.Checkout;

namespace ParadiseCommerce.Services.Billing.Controllers.Gateways
{
    [Route("api/gateways/[controller]")]
    [ApiController]
    public class StripeWebhooks : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<StripeWebhooks> _logger;
        private readonly IBillService _billService;

        public StripeWebhooks(IConfiguration configuration, IInvoiceRepository invoiceRepository, ILogger<StripeWebhooks> logger, IBillService billService)
        {
            _configuration = configuration;
            _invoiceRepository = invoiceRepository;
            _logger = logger;
            _billService = billService;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Received Stripe webhook!");
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _configuration["PaymentGateways:Stripe:WebhookSecret"]
                );

                switch (stripeEvent.Type)
                {
                    case Events.CheckoutSessionCompleted:
                        var session = stripeEvent.Data.Object as Session;
                        if (session == null) return BadRequest();

                        _logger.LogInformation($"Checkout session {session.Id} completed!");

                        if (!session.Metadata.TryGetValue("InvoiceId", out var invoiceId))
                        {
                            _logger.LogError("Received a CheckoutSessionCompleted event, but InvoiceId is not defined in metadata! Ignoring..");
                            return BadRequest();
                        }

                        var invoice = await _invoiceRepository.Get(invoiceId);
                        if (invoice == null)
                        {
                            _logger.LogError($"Could not find invoice with id {invoiceId}!");
                            return BadRequest();
                        }

                        await _billService.SetInvoicePaid(invoice, "stripe");
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}