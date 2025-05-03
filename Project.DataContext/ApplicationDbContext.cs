
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using Project.Repositories.Entities;
using Project.Repositories.Repositories;

namespace Project.DataContext
{
    public class ApplicationDbContext : DbContext, IContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<DeleteTokenDto> DeleteTokens { get; set; }
        public DbSet<RefreshTokenDto> RefreshTokens { get; set; }

        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(local)\SQLEXPRESS;database=Educational_forum1;trusted_connection=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Post entity relationships using Fluent API
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Counselor)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CounselorId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Topic>()
                .Property(t => t.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Post>()
                .Property(t => t.Date)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}

