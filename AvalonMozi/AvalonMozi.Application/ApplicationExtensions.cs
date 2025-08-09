using AvalonMozi.Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AvalonMozi.Application
{
    public static class ApplicationExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
