using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/prodiver")]
    public class ProviderController : Controller
    {
        private readonly ProviderService _providerService;

        public ProviderController(ProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet("{name}")]
        [Route("create")]
        public async Task<IActionResult> Create(string name)
        {
            if (Request.Headers["Role"] != "Admin")
            {
                return BadRequest();
            }

            await _providerService.Create(name);

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

            await _providerService.Delete(id);

            return Ok();
        }
    }
}
