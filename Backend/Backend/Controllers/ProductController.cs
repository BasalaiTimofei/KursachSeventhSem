using System.Linq;
using System.Threading.Tasks;
using Backend.Models.View;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(string id)
        {
            return Ok(await _productService.GetById(id));
        }

        [HttpPost]
        [Route("property")]
        public async Task<IActionResult> GetByProperty([FromBody] PropertyViewModel property)
        {
            var result = await _productService.GetByProperty(property);

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (Request.Headers["Role"] != "Admin")
            {
                return BadRequest();
            }

            await _productService.Create(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (Request.Headers["Role"] != "Admin")
            {
                return BadRequest();
            }

            await _productService.Delete(id);

            return Ok();
        }
    }
}