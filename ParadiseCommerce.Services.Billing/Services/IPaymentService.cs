using System.Threading.Tasks;
using ParadiseCommerce.Services.Billing.Models;
using ParadiseCommerce.Services.Contracts;

namespace ParadiseCommerce.Services.Billing.Services
{
    public interface IPaymentService
    {
        Task<PaymentSchema> CreatePaymentLink(Invoice invoice, string successUrl, string failUrl);
    }
}