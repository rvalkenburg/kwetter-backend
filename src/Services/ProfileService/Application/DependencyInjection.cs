﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.ProfileService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}