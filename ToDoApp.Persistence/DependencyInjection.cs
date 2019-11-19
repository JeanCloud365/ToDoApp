using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ToDoDbContext>(options => options.UseSqlite("Data Source=data.db"));

            services.AddScoped<IToDoDbContext>(provider => provider.GetService<ToDoDbContext>());

            return services;
        }
    }
}
