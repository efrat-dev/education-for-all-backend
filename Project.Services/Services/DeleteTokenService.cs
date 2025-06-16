using Common.Dto;
using Project.DataContext;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project.Services.Services
{
    public class DeleteTokenService : IDeleteTokenService
    {
        private readonly ApplicationDbContext context;

        public DeleteTokenService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> GenerateTokenAsync(int postId)
        {
            var token = Guid.NewGuid().ToString();
            var deleteToken = new DeleteTokenDto
            {
                PostId = postId,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(48)
            };
            context.DeleteTokens.Add(deleteToken);
            await context.SaveChangesAsync();
            return token;
        }

        public async Task<DeleteTokenDto?> GetTokenAsync(string token)
        {
            return await context.DeleteTokens
                .FirstOrDefaultAsync(t => t.Token == token && t.Expiration > DateTime.UtcNow);
        }

        public async Task<bool> DeleteTokenAsync(string token)
        {
            var tokenEntity = await context.DeleteTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (tokenEntity == null) return false;

            context.DeleteTokens.Remove(tokenEntity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
