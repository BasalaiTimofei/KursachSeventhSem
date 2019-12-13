using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/database")]
    public class DatabaseController : Controller
    {
        private readonly Creating _creating;
        public DatabaseController(Creating creating)
        {
            _creating = creating;
        }

        [HttpGet]
        [Route("provider")]
        public async Task<IActionResult> Provider()
        {
            await _creating.CreateProvider();
            return Ok();
        }

        [HttpGet]
        [Route("role")]
        public async Task<IActionResult> Role()
        {
            await _creating.CreateRole();
            return Ok();
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> User()
        {
            await _creating.CreateUser();
            return Ok();
        }

        [HttpGet]
        [Route("product")]
        public async Task<IActionResult> Product()
        {
            await _creating.CreateProduct();
            return Ok();
        }

        [HttpGet]
        [Route("assessment")]
        public async Task<IActionResult> Assessment()
        {
            await _creating.CreateAssessment();
            return Ok();
        }

        [HttpGet]
        [Route("comment")]
        public async Task<IActionResult> Comment()
        {
            await _creating.CreateComment();
            return Ok();
        }

        [HttpGet]
        [Route("order")]
        public async Task<IActionResult> Order()
        {
            for (int i = 0; i < 5; i++)
            {
                await _creating.CreateOrder();  
            }
            return Ok();
        }

        [HttpGet]
        [Route("basket")]
        public async Task<IActionResult> Basket()
        {
            await _creating.CreateBasket();
            return Ok();
        }

        [HttpGet]
        [Route("admin")]
        public async Task<IActionResult> Admin()
        {
            await _creating.CreateAdmin();
            return Ok();
        }
    }
}