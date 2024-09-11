using Directory.Models.Requests;
using Directory.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Directory.Controllers
{
    [ApiController]
    [Route("directory/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        // Route -> Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var registerResult = await _authService.RegisterAsync(register);
            if (registerResult.IsSucceed)
            {
                return Ok(registerResult);
            }
            return BadRequest(registerResult);
        }
    }
}