using AvalonMozi.Application.Feedbacks;
using AvalonMozi.Application.Movies.Services;
using AvalonMozi.Application.Orders.Services;
using AvalonMozi.Application.Tickets.Services;
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
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
        }
    }
}
