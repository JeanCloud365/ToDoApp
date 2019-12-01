using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
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
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using ToDoApp.Api.Services;
using ToDoApp.Application;
using ToDoApp.Application.Common.Behaviors;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;
using ToDoApp.Infrastructure;
using ToDoApp.Persistence;
using AuthenticationOptions = ToDoApp.Api.Common.AuthenticationOptions;

namespace ToDoApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IServiceCollection _services;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddInfrastructure();
            services.AddHealthChecks()
                .AddDbContextCheck<ToDoDbContext>();
            services.AddHttpContextAccessor();

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IToDoDbContext>());

            services.AddRazorPages();
            var swaggerOAuthFlow = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = "https://appdatadev.b2clogin.com/appdatadev.onmicrosoft.com/B2C_1_signupsignin/oauth2/v2.0/authorize",
                TokenUrl = "https://appdatadev.b2clogin.com/appdatadev.onmicrosoft.com/B2C_1_signupsignin/oauth2/v2.0/token",
                RefreshUrl = "https://appdatadev.b2clogin.com/appdatadev.onmicrosoft.com/B2C_1_signupsignin/oauth2/v2.0/token",
                Scopes = new Dictionary<string, string>
                {
                    {"https://appdatadev.onmicrosoft.com/todo/ReadAll", "Read All"},
                    {"offline_access","offline_access"},
                    { "email","email"}
                }
            };
            var swaggerSecurity = new OpenApiSecurityScheme()
            {

                Flows = new OpenApiOAuthFlows()
                {
                  //  Implicit = swaggerOAuthFlow,
                    AuthorizationCode = swaggerOAuthFlow
                },


                Type = OpenApiSecuritySchemeType.OAuth2,




            };
            services.AddSwaggerDocument(o =>
                {
                    o.PostProcess = s =>
                    {
                        s.Host = Configuration["SwaggerHost"];
                        s.BasePath = "/";
                        s.Schemes = new List<OpenApiSchema>()
                        {
                            OpenApiSchema.Https
                        };
                        s.SecurityDefinitions.Add("oauth2code", swaggerSecurity);

                    };
                    o.Title = "ToDo App";
                    o.Description = "Yes...Another Todo App";

                    o.OperationProcessors.Add(new OperationSecurityScopeProcessor("oauth2code"));
                    //o.DocumentProcessors.Add(new SecurityDefinitionAppender("oauth2",swaggerSecurity));
                }


            );


            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AuthenticationOptions");
            services.Configure<AuthenticationOptions>(appSettingsSection);

            // configure jwt authentication
            var authenticationOptions = appSettingsSection.Get<AuthenticationOptions>();
            services.AddAuthentication(o => o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Authority = authenticationOptions.Authority;
                    o.Audience = authenticationOptions.Audience;
                    o.TokenValidationParameters.ValidateAudience = false;
                    o.TokenValidationParameters.ValidateIssuer = false;

                });






            services.AddAuthorization(o =>
            {
                o.AddPolicy("ReadAll", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
            });

            _services = services;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                RegisteredServicesPage(app);
            }
            else
            {
                // app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseOpenApi();

            app.UseSwaggerUi3(settings =>
                {
                    settings.Path = "/api";
                    settings.DocumentPath = "api/specification.json";  // Enable when NSwag.MSBuild is upgraded to .NET Core 3.0
                    settings.OAuth2Client = new OAuth2ClientSettings();
                    settings.OAuth2Client.ClientId = "80036b40-f42b-4e8a-834b-08165b90d1b9";
                    settings.OAuth2Client.ClientSecret = "v!>LcJbI7N[7d95|n\\dS9C1G";
                    //settings.OAuth2Client.Realm =
                    //  "https://login.microsoftonline.com/tfp/b6110487-86e3-418f-aba6-7f26f3bccc48/B2C_1_SignUpSignIn/v2.0/";
                    //settings.OAuth2Client.AdditionalQueryStringParameters.Add("p", "B2C_1_SignUpSignIn");

                });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void RegisteredServicesPage(IApplicationBuilder app)
        {
            app.Map("/services", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>Registered Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }


}