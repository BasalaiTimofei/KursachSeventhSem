using System;
using System.Threading.Tasks;
using Backend.Models.View;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/assessment")]
    public class AssessmentController : Controller
    {
        private readonly AssessmentService _assessmentService;

        public AssessmentController(AssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] AssessmentViewModel model)
        {
            await _assessmentService.Create(Request.Headers["UserId"], model.ProductId, Convert.ToByte(model.Value));

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] AssessmentViewModel model)
        {
            await _assessmentService.Update(Request.Headers["UserId"], model.ProductId, Convert.ToByte(model.Value));

            return Ok();
        }
    }
}
