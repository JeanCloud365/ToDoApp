using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Infrastructure.Persistence;
using ToDoApp.Infrastructure.Services;

namespace ToDoApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<INotificationService, NotificationService>();

            services.AddDbContext<ToDoDbContext>(options =>
            {
                options.UseSqlServer(configuration["SqlConnectionString"]);
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IToDoDbContext>(provider => provider.GetService<ToDoDbContext>());

            services.AddTransient<IDateTimeService, DateTimeService>();

            return services;
        }

    }
}
