using Microsoft.AspNetCore.Mvc;
using Common.Dto;
using Project.Services.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LogInController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequestDto loginModel)
        {
            return await _loginService.SignInAsync(loginModel);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            return await _loginService.RefreshTokenAsync();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            return await _loginService.LogoutAsync(refreshToken);
        }
    }
}
