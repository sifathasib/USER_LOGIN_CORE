using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using USER_LOGIN.Services;

namespace USER_LOGIN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // This ensures only authenticated requests (with valid JWT) can access these endpoints
    public class ValuesController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public ValuesController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // Example endpoint that returns protected data
        [HttpGet("getinfo")]
        public IActionResult GetInfo()
        {
            // Option 1: Use HttpContext.User which is automatically set by JWT middleware
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            // Option 2: You can also extract and validate the token manually if needed.
            // For demonstration, suppose we retrieve the token from the Authorization header:
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
            {
                string token = authHeader.Substring("Bearer ".Length).Trim();
                var principal = _tokenService.ValidateToken(token);
                if (principal != null)
                {
                    username = principal.FindFirst(ClaimTypes.Name)?.Value;
                }
            }

            return Ok(new { Message = "This is protected data.", Username = username });
        }
    }
}