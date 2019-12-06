using System.Threading.Tasks;
using Backend.Models.View;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProduct(string id)
        {
            var result = await _commentService.GetByProduct(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] CommentViewModel model)
        {
            await _commentService.Create(Request.Headers["UserId"], model.ProductId, model.Text);

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(CommentViewModel model)
        {
            await _commentService.Update(model.Id, model.Text);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            await _commentService.Delete(id);

            return Ok();
        }
    }
}