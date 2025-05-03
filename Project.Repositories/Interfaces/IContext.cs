using Microsoft.EntityFrameworkCore;
using Project.Repositories.Entities;

namespace Project.Repositories.Repositories
{
    public interface IContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public Task Save();
    }
}
