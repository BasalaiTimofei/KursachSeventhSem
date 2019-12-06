using System;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.View;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserService _userService;

        public UserController(ApplicationContext applicationContext, UserService userService)
        {
            _applicationContext = applicationContext;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel model)
        {
            try
            {
                var result = await _userService.Login(model);
                Response.Headers.Add("UserId", result[0]);
                Response.Headers.Add("Role", result[1]);

                return Ok();
            }
            catch (Exception e) //TODO Свой Exception
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Registration([FromBody] RegistrationUserViewModel model)
        {
            try
            {
                var result = await _userService.Registration(model, "User");
                Response.Headers.Add("UserId", result[0]);
                Response.Headers.Add("Role", result[1]);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteUserViewModel model)
        {
            try
            {
                if (Request.Headers["Role"] == "Admin" || Request.Headers["UserId"] ==
                    _applicationContext.Users.FindAsync(Request.Headers["UserId"]).Result.Id)
                {
                    await _userService.Delete(model);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}