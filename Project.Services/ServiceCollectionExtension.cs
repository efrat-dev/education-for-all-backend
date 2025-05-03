using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Project.Repositories;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddRepository();
            services.AddScoped<IUserService<CounselorDto>, CounselorService>();
            services.AddScoped<IUserService<UserDto>, UserService>();
            services.AddScoped<IForumService<TopicDto>, TopicService>();
            services.AddScoped<IForumService<PostDto>, PostService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITopicReadingService, TopicReadingService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddAutoMapper(typeof(MapProfile));
            return services;
        }
    }
}
