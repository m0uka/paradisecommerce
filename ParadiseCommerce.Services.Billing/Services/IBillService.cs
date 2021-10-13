using System.Threading.Tasks;
using MassTransit;
using ParadiseCommerce.Services.Billing.Models;
using ParadiseCommerce.Services.Contracts;

namespace ParadiseCommerce.Services.Billing.Services
{
    public interface IBillService
    {
        Task<Invoice> CreateInvoice(BillCommand billCommand);
        Task<PaymentSchema> CreateInvoicePaymentSchema(Invoice invoice);
        
        Task SetInvoicePaid(Invoice invoice, string paymentMethod);
    }
}