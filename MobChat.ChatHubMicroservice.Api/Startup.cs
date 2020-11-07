using System;
using System.Collections.Generic;
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
using MobChat.ChatHubMicroservice.Api.Helpers;
using MobChat.ChatHubMicroservice.Api.Hubs;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using MobChat.ChatHubMicroservice.Infra.DataAccess;
using MobChat.ChatHubMicroservice.Infra.Repositories.ConnectedUsers;
using MobChat.ChatHubMicroservice.Infra.Repositories.OfflineTextMessages;

namespace MobChat.ChatHubMicroservice.Api
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
            services.AddControllers();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.Authority = "https://mobchat.b2clogin.com/tfp/8d851549-9c94-4f28-9092-b076089985f9/B2C_1_mobchat_signupandsignin/v2.0/";
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
