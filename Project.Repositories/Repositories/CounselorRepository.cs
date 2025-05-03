using Microsoft.EntityFrameworkCore;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Repositories
{
    public class CounselorRepository : IUserRepository<Counselor>
    {
        private readonly IContext context;

        public CounselorRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Counselor> AddAsync(Counselor counselor)
        {
            await context.Counselors.AddAsync(counselor);
            await context.Save();
            return counselor;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var counselor = await context.Counselors.FirstOrDefaultAsync(c => c.Id == id);
            if (counselor != null)
            {
                context.Counselors.Remove(counselor);
                await context.Save();
            }
        }

        public async Task<Counselor?> GetByIdAsync(int id)
        {
            return await context.Counselors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Counselor>> GetAllUsersAsync()
        {
            return await context.Counselors.ToListAsync();
        }

        public async Task<Counselor> UpdateAsync(Counselor entity)
        {
            context.Counselors.Update(entity);
            await context.Save();
            return entity;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await context.Counselors.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await context.Counselors.AnyAsync(u => u.Name == userName);
        }
    }
}
