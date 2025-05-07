using Common.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService<UserDto> _userService;
        private readonly IUserService<CounselorDto> _counselorService;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LoginService(
            IUserService<UserDto> userService,
            IUserService<CounselorDto> counselorService,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _userService = userService;
            _counselorService = counselorService;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        // Extracts the ID of the given user object as a string
        public string GetUserId(object user) => user switch
        {
            UserDto userDto => userDto.Id.ToString(),
            CounselorDto counselorDto => counselorDto.Id.ToString(),
            _ => throw new InvalidOperationException("Invalid user type")
        };

        // Determines the role ("user" or "counselor") based on the type of the user object
        public string GetUserRole(object user) => user switch
        {
            UserDto => "user",
            CounselorDto => "counselor",
            _ => throw new InvalidOperationException("Invalid user type")
        };

        // Attempts to authenticate a user or counselor by name and password
        public async Task<object?> AuthenticateAsync(string name, string password)
        {
            var counselors = await _counselorService.GetAllAsync();
            var counselor = counselors.FirstOrDefault(x => x.Name == name && BCrypt.Net.BCrypt.Verify(password, x.Password));
            if (counselor != null) return counselor;

            var users = await _userService.GetAllAsync();
            return users.FirstOrDefault(x => x.Name.ToLower() == name.ToLower() && BCrypt.Net.BCrypt.Verify(password, x.Password));
        }

        // Signs in the user and issues a new access and refresh token; stores the refresh token as a secure cookie
        public async Task<IActionResult> SignInAsync(LoginRequestDto loginModel)
        {
            var user = await AuthenticateAsync(loginModel.Name, loginModel.Password);
            if (user != null)
            {
                var userId = GetUserId(user);
                var role = GetUserRole(user);
                var accessToken = _tokenService.GenerateAccessToken(userId, role);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(int.Parse(userId));
                var environment = _configuration["ASPNETCORE_ENVIRONMENT"];

                // Define cookie options based on the environment (Development or Production)
                var cookieOptions = new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                };

                if (environment == "Development")
                {
                    cookieOptions.SameSite = SameSiteMode.None;
                    cookieOptions.HttpOnly = false;
                }

                else if (environment == "Production")
                {
                    cookieOptions.SameSite = SameSiteMode.Strict;
                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
                return new OkObjectResult(new { AccessToken = accessToken });
            }

            return new StatusCodeResult(StatusCodes.Status404NotFound); // No user found
        }

        // Refreshes the access token using a valid refresh token from cookies
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken)) return new StatusCodeResult(StatusCodes.Status401Unauthorized);

            var (userId, role) = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
            if (userId == null || role == null) return new StatusCodeResult(StatusCodes.Status401Unauthorized);

            var newAccessToken = _tokenService.GenerateAccessToken(userId, role);

            var environment = _configuration["ASPNETCORE_ENVIRONMENT"];
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            if (environment == "Development")
            {
                cookieOptions.SameSite = SameSiteMode.None; 
                cookieOptions.HttpOnly = false; 
            }
            else if (environment == "Production")
            {
                cookieOptions.SameSite = SameSiteMode.Strict; 
            }

            return new OkObjectResult(new { accessToken = newAccessToken });
        }

        // Revokes the refresh token and logs the user out
        public async Task<IActionResult> LogoutAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return new StatusCodeResult(StatusCodes.Status400BadRequest);

            var success = await _tokenService.RevokeRefreshTokenAsync(refreshToken);
            if (!success) return new StatusCodeResult(StatusCodes.Status401Unauthorized);

            return new OkObjectResult("Logged out successfully.");
        }
    }
}
