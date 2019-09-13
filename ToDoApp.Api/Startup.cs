using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToDoApp.Api.Filters;
using ToDoApp.Api.Infrastructure;
using ToDoApp.Application.Infrastructure;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;
using ToDoApp.Persistence;

namespace ToDoApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddTransient<ICurrentUser, CurrentUser>();
            
            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateToDoUserCommandValidator>());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddDbContext<IToDoDbContext, ToDoDbContext>(options => options.UseSqlite("Data Source=data.db"));
            services.AddSwaggerDocument();
           
          
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddAuthentication(o => o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Authority =
                        "https://login.microsoftonline.com/tfp/b6110487-86e3-418f-aba6-7f26f3bccc48/B2C_1_SignUpSignIn/v2.0/";
                    o.Audience = "63d633d2-d568-4d9c-ad0f-e111b8220cf8";
                    o.TokenValidationParameters.ValidateAudience = false;
                   
                    
                    
                   
                  
                });
            services.AddAuthorization(o =>
            {
                o.AddPolicy("ReadAll",new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
            });
            services.AddMediatR(typeof(ListToDoUsersQuery).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public class MyJwtBearerEvents:JwtBearerEvents
    {
       /* private IMediator _mediator;
        private readonly IServiceProvider _provider;

        public MyJwtBearerEvents(IMediator mediator,IServiceProvider provider)
        {
            _mediator = mediator;
            _provider = provider;
        }
        public override Task TokenValidated(TokenValidatedContext context)
        {
            
            using(var scope = _provider.CreateScope())
            {
                scope.ServiceP
                // Resolve the Scoped service
                var service = scope.ServiceProvider.GetService<>();
                options.MyValue = service.GetValue();
            }
            
            var result = _mediator.Send(new CreateToDoUserCommand()).Result;
            return Task.FromResult(0);
        } */
    }
}