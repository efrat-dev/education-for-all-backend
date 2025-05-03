using Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Project.Services.Interfaces
{
    public interface ILoginService
    {
        Task<object?> AuthenticateAsync(string name, string password);
        Task<IActionResult> SignInAsync(LoginRequestDto loginModel);
        Task<IActionResult> RefreshTokenAsync();
        Task<IActionResult> LogoutAsync(string refreshToken);
    }
}
