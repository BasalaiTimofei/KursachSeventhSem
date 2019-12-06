using System.Linq;
using System.Threading.Tasks;
using Backend.Models.View;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/basket")]
    public class BasketController : Controller
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (Request.Headers.All(w => w.Key != "UserId")) return BadRequest();
            var result = await _basketService.Get(Request.Headers["UserId"]);

            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] BasketViewModel model)
        {
            await _basketService.Add(Request.Headers["UserId"], model.ProductId);

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] BasketViewModel model)
        {
            await _basketService.Delete(Request.Headers["UserId"], model.ProductId);

            return Ok();
        }
    }
}