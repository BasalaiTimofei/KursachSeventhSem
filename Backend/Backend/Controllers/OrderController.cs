using System.Threading.Tasks;
using Backend.Models.View;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly BasketService _basketService;

        public OrderController(OrderService orderService, BasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAll());
        }

        [HttpGet("{id}")]
        [Route("create")]
        public async Task<IActionResult> Create(string id)
        {
            await _orderService.Create(Request.Headers["UserId"], new[] {id});

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] OrderViewModel model)
        {
            await _orderService.Create(Request.Headers["UserId"], model.ProductId);
            await _basketService.Delete(Request.Headers["UserId"], model.ProductId);

            return Ok();
        }
    }
}