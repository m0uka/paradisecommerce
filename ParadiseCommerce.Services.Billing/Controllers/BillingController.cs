using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParadiseCommerce.Services.Billing.Models;
using ParadiseCommerce.Services.Billing.Repositories;

namespace ParadiseCommerce.Services.Billing.Controllers.Gateways
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public BillingController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("invoices")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var invoices = await _invoiceRepository.GetByUser(userId);
            return Ok(invoices);
        }
        
        [HttpGet("invoices/order/{id}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByOrder(string id)
        {
            var invoice = await _invoiceRepository.GetByOrder(id);
            return Ok(invoice);
        }
    }
}