using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoApp.UI.Data;

namespace ToDoApp.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
          
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var api = Configuration.GetValue<string>("api") ?? "https://jvm-todo-api.azurewebsites.net";
            var baseRedirectUrl = Configuration.GetValue<string>("baseRedirectUrl") ?? "https://jvm-todo-web.azurewebsites.net";
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    
                    options.Scope.Add("openid");
                    options.Scope.Add(("profile"));
                    options.Scope.Add("https://appdatadev.onmicrosoft.com/todo/ReadAll");
                    options.ResponseType = "id_token token";
                    options.SaveTokens = true;
                    options.ClientId = "b586b614-2269-4b14-b08d-820c5e76a79c";
                    options.AuthenticationMethod = OpenIdConnectRedirectBehavior.FormPost;
                    options.Authority =
                        "https://login.microsoftonline.com/tfp/b6110487-86e3-418f-aba6-7f26f3bccc48/B2C_1_SignUpSignIn/v2.0";
                    options.UseTokenLifetime = true;
                    options.Events = new OpenIdConnectEvents()
                    {
                        OnRedirectToIdentityProvider = new Func<RedirectContext, Task>(o =>
                        {
                            o.ProtocolMessage.RedirectUri = baseRedirectUrl + "/signin-oidc";
                            return Task.FromResult(0);
                        }),
                        OnTicketReceived = new Func<TicketReceivedContext, Task>(async o =>
                        {
                            var token = o.Properties.GetTokenValue("access_token");
                            using HttpClient httpClient = new HttpClient()
                            {
                                BaseAddress = new Uri(api),
                                DefaultRequestHeaders = {Authorization = new AuthenticationHeaderValue("Bearer", token)}
                            };
                            var client = new UsersClient(httpClient);
                            client.BaseUrl = api;

                            await client.CreateAsync(new CreateToDoUserCommand());
                        })
                    };
                    

                });
            services.AddMvcCore(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddHttpClient()
            services.AddHttpClient<IItemsClient,ItemsClient>(async (serviceProvider, client) =>
            {
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                if (httpContextAccessor.HttpContext == null)
                    return;
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                if (accessToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    client.BaseAddress = new Uri(api);
                }
            });
            
            services.AddHttpClient<IUsersClient,UsersClient>(async (serviceProvider, client) =>
            {
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                client.BaseAddress = new Uri(api);
                
            });
            
            services.AddHttpClient<IAdminClient,AdminClient>(async (serviceProvider, client) =>
            {
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                client.BaseAddress = new Uri(api);
            });
            
           

            services.AddRazorPages();
            services.AddServerSideBlazor();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}