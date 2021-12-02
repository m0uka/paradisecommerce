using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParadiseCommerce.Services.Ordering.Models;
using ParadiseCommerce.Services.Ordering.Repositories;

namespace ParadiseCommerce.Services.Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        [HttpPost("update")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Update([FromBody] ProductCategory category)
        {
            var id = await _categoryRepository.Update(category.Id, category);
            return new JsonResult(id.ToString());
        }
        
        [HttpPost("create")]
        // [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([FromBody] ProductCategory category)
        {
            var id = await _categoryRepository.Create(category);
            return new JsonResult(id.ToString());
        }
        
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var categories = await _categoryRepository.Get(id);
            return new JsonResult(categories);
        }
        
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.Get();
            return new JsonResult(categories);
        }
    }
}