using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IDeleteTokenService
    {
        Task<string> GenerateTokenAsync(int postId);
        Task<DeleteTokenDto?> GetTokenAsync(string token);
        Task<bool> DeleteTokenAsync(string token);
    }
}
