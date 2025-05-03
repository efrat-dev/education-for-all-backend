
using Microsoft.Extensions.DependencyInjection;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;
using Project.Repositories.Repositories;

namespace Project.Repositories
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IForumRepository<Topic>, TopicRepository>();
            services.AddScoped<IUserRepository<User>, UserRepository>();
            services.AddScoped<IUserRepository<Counselor>, CounselorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
