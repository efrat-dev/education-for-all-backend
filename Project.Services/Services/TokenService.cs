using Microsoft.IdentityModel.Tokens;
using Project.DataContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Common.Dto;
using Microsoft.Extensions.Configuration;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly ApplicationDbContext dbContext;

        public TokenService(IConfiguration config, ApplicationDbContext dbContext)
        {
            this.config = config;
            this.dbContext = dbContext;
        }

        public string GenerateAccessToken(string userId, string role)
        {
            var keyString = config["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("Jwt:Key is not set in the configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshTokenAsync(int userId)
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var refreshToken = Convert.ToBase64String(randomBytes);

            var refreshTokenEntity = new RefreshTokenDto
            {
                UserId = userId,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.RefreshTokens.Add(refreshTokenEntity);
            await dbContext.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<(string?, string?)> ValidateRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await dbContext.RefreshTokens
                .Where(rt => rt.Token == refreshToken && !rt.IsRevoked && rt.ExpiryDate > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (storedToken == null)
                return (null, null);

            string? role = await dbContext.Users.AnyAsync(u => u.Id == storedToken.UserId) ? "User" :
                          await dbContext.Counselors.AnyAsync(c => c.Id == storedToken.UserId) ? "Counselor" : null;

            return role == null ? (null, null) : (storedToken.UserId.ToString(), role);
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await dbContext.RefreshTokens
                .Where(rt => rt.Token == refreshToken && !rt.IsRevoked && rt.ExpiryDate > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (storedToken == null)
                return false;

            storedToken.IsRevoked = true;
            storedToken.RevokedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
