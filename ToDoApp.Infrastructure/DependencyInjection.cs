using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<INotificationService,NotificationService>();

            return services;
        }

    }
}
