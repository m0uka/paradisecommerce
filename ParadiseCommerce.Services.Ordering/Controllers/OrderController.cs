using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParadiseCommerce.Contracts;
using ParadiseCommerce.Services.Contracts;
using ParadiseCommerce.Services.Ordering.Models;
using ParadiseCommerce.Services.Ordering.Repositories;

namespace ParadiseCommerce.Services.Ordering.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(ISendEndpointProvider sendEndpointProvider, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _orderRepository.Get(id);

            if (order.UserId != userId) return Unauthorized();
            
            return Ok(order);
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = await _orderRepository.GetByUser(userId);
            return Ok(orders);
        }
        
        [HttpPost("place")]
        public async Task<IActionResult> Create([FromBody] OrderPlaceModel orderModel)
        {
            if (orderModel.ProductInfos.Count == 0) return BadRequest("no_items");

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            string usedCurrency = orderModel.Currency;
            decimal totalCost = 0;

            // Check if any of the products is a physical product, then require a shipping address,
            // and calculate the price

            Dictionary<string, Product> products = new();
            foreach (var orderItem in orderModel.ProductInfos)
            {
                var product = await _productRepository.Get( orderItem.ProductId );
                if (product == null) return BadRequest("invalid_item");
                if (orderItem.Quantity < 1) return BadRequest("invalid_quantity"); // TODO: Check for stock later (we'll have to ask the provisioning module)

                if (product.ProductType == ProductType.Physical && orderModel.ShippingAddress == null)
                    return BadRequest("no_shipping_address");

                if (!product.Pricing.CurrencyPrices.TryGetValue(usedCurrency, out var price))
                {
                    return BadRequest("currency_not_supported");
                }

                products[orderItem.ProductId] = product;
                totalCost += price * orderItem.Quantity;
            }

            var order = new Order
            {
                UserId = userId,
                
                Amount = totalCost,
                Currency = usedCurrency,
                UsedPromo = orderModel.PromoCode,
                
                Products = orderModel.ProductInfos,
                
                PaymentMethod = orderModel.PaymentMethod, // check if valid payment method??
                Status = OrderStatus.WaitingForPayment,
                ShippingAddress = orderModel.ShippingAddress,
                
                CreatedAt = DateTime.Now
            };

            await _orderRepository.Create(order);

            // TODO: Redirect user to billing service, where user can pay for the order
            
            var billingEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:billing-service"));
            var billCommand = new BillCommand
            {
                OrderId = order.Id.ToString(),
                Currency = order.Currency,
                PaymentMethod = order.PaymentMethod,
                
                CustomerInfo = new BillCustomerInfo
                {
                    CustomerId = userId,
                    CustomerEmail = userEmail
                },
                
                Items = new List<BillItem>()
            };

            foreach (var orderItem in orderModel.ProductInfos)
            {
                var product = products[orderItem.ProductId];
                
                var billItem = new BillItem
                {
                    Id = orderItem.ProductId,
                    Name = product.Name,
                    Description = product.ShortDescription,
                    UnitAmount = product.Pricing.CurrencyPrices[usedCurrency],
                    Quantity = orderItem.Quantity
                };
                
                billCommand.Items.Add(billItem);
            }
            
            await billingEndpoint.Send(billCommand);
            return Ok(order.Id);
        }
    }
}