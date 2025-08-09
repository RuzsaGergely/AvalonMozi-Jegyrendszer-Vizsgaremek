﻿using AvalonMozi.Factories.UserFactories;
using Microsoft.Extensions.DependencyInjection;

namespace AvalonMozi.Factories
{
    public static class FactoryExtensions
    {
        public static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IUserFactory, UserFactory>();
            services.AddTransient<IRoleFactory, RoleFactory>();
        }
    }
}
