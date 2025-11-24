using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProduct(Guid? ProductId)
        {
            return Ok();
        }
    }
}
