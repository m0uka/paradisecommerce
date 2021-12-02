using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpPost("update")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            var id = await _productRepository.Update(product.Id, product);
            return new JsonResult(id.ToString());
        }
        
        [HttpPost("create")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var id = await _productRepository.Create(product);
            return new JsonResult(id.ToString());
        }
        
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategory(string id)
        {
            var product = await _productRepository.GetByGroup(id);
            return new JsonResult(product);
        }
        
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.Get(id);
            return new JsonResult(product);
        }
        
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.Get();
            return new JsonResult(products);
        }
    }
}