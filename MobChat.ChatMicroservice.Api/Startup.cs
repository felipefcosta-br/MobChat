using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MobChat.ChatMicroservice.Api.Hubs;
using MobChat.ChatMicroservice.Api.Providers;
using MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using MobChat.ChatMicroservice.Infra.Repositories.ConnectedUsers;
using MobChat.ChatMicroservice.Infra.Repositories.OfflineTextMessages;

namespace MobChat.ChatMicroservice.Api
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllers();

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(jwtOptions =>
            {
                
                jwtOptions.Authority = $"https://mobchat.b2clogin.com/{Configuration["AzureAdB2C:Tenant"]}/{Configuration["AzureAdB2C:Policy"]}/v2.0";
                jwtOptions.Audience = Configuration["AzureAdB2C:ClientId"];
                jwtOptions.Events = new JwtBearerEvents
                {
                    
                    //OnAuthenticationFailed = abcd;
                };

                jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudiences = new List<string>() {
                            "8d851549-9c94-4f28-9092-b076089985f9"
                        }
                };

                jwtOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs/chathub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            services.AddScoped<IConnectedUserRepository, AzureSqlServerConnectedUserRepository>();
            services.AddScoped<IOfflineTextMessageRepository, AzureSqlServerOfflineTextMessageRepository>();
            services.AddScoped<IConnectedUserService, ConnectedUserService>();
            services.AddScoped<IOfflineTextMessageService, OfflineTextMessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/Hubs/ChatHub");
            });
        }
    }
}
