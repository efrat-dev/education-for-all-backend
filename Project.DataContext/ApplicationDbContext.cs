
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Repositories.Entities;
using Project.Repositories.Repositories;

namespace Project.DataContext
{

    public class ApplicationDbContext : DbContext, IContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<DeleteTokenDto> DeleteTokens { get; set; }
        public DbSet<RefreshTokenDto> RefreshTokens { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["DefaultConnection"];
            optionsBuilder.UseNpgsql(connectionString);
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
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Post>()
                .Property(t => t.Date)
                .HasDefaultValueSql("NOW()");

            // Apply UTC conversion for DateTime properties
            modelBuilder.Entity<Topic>()
                .Property(t => t.DateCreated)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            modelBuilder.Entity<Post>()
                .Property(t => t.Date)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

