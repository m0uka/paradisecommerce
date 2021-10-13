using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;
using ParadiseCommerce.Services.Ordering.Repositories;

namespace ParadiseCommerce.Services.Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var id = await _productRepository.Create(product);
            return new JsonResult(id.ToString());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.Get(ObjectId.Parse(id));
            return new JsonResult(product);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.Get();
            return new JsonResult(products);
        }
    }
}