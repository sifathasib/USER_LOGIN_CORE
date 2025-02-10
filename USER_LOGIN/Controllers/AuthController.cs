using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using USER_LOGIN.Models;
using USER_LOGIN.Services;

namespace USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.userId) || string.IsNullOrEmpty(userLogin.userPass))
            {
                return BadRequest(new { Message = "Username and password cannot be empty" });
            }
            var token = await _userService.ValidateUserAsync(userLogin);
            
            if (token == null)
                return Unauthorized(new { Message = "Invalid username or password" });
            return Ok(new { Token = token });
        }
    }

    
}