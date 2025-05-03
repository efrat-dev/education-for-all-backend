using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string role);
        Task<string> GenerateRefreshTokenAsync(int userId);
        Task<(string?, string?)> ValidateRefreshTokenAsync(string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }
}
