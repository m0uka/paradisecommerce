using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParadiseCommerce.Services.Storefront.Repositories;

namespace ParadiseCommerce.Services.Storefront.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStorefrontRepository _storefrontRepository;

        public StoreController(IStorefrontRepository storefrontRepository)
        {
            _storefrontRepository = storefrontRepository;
        }
        
        [HttpGet("storefront")]
        public async Task<ActionResult<Models.Storefront>> GetStorefront()
        {
            var storefront = await _storefrontRepository.Get();
            return Ok(storefront);
        }
        
        [HttpPost("update")]
        public async Task<ActionResult<Models.Storefront>> UpdateStorefront([FromBody] Models.Storefront storefront)
        {
            await _storefrontRepository.Update(storefront);
            return Ok();
        }
    }
}