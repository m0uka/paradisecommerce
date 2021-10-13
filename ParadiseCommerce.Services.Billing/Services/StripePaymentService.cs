using System.Collections.Generic;
using System.Threading.Tasks;
using ParadiseCommerce.Services.Billing.Models;
using ParadiseCommerce.Services.Contracts;
using Stripe.Checkout;

namespace ParadiseCommerce.Services.Billing.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        public async Task<PaymentSchema> CreatePaymentLink(Invoice invoice, string successUrl, string failUrl)
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = failUrl,
                Locale = "cs",

                // Customer = command.CustomerInfo.CustomerId,
                CustomerEmail = invoice.CustomerEmail,
                Metadata = new()
                {
                    { "InvoiceId", invoice.Id.ToString() },
                    { "CustomerId", invoice.CustomerId.ToString() },
                }
            };

            foreach (var item in invoice.BillItems)
            {
                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new()
                    {
                        Currency = invoice.Currency,
                        UnitAmountDecimal = item.UnitAmount * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = item.Name,
                            Description = item.Description,
                        }
                    },
                    Quantity = item.Quantity
                };
                
                options.LineItems.Add(lineItem);
            }

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new PaymentSchema
            {
                PaymentUrl = session.Url
            };
        }
    }
}