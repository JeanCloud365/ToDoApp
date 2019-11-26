using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ToDoDbContext>(options => options.UseSqlite("Data Source=" + configuration["Data"]));

            services.AddScoped<IToDoDbContext>(provider => provider.GetService<ToDoDbContext>());

            return services;
        }
    }
}
