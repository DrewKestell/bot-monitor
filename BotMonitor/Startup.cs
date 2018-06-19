using BotMonitor.Data;
using BotMonitor.Models;
using BotMonitor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotMonitor
{
    public class Startup
    {
        readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("BloogBotAuthenticationScheme")
                .AddCookie(
                    "BloogBotAuthenticationScheme",
                    options =>
                    {
                        options.AccessDeniedPath = "/Home/AccessDenied/";
                        options.LoginPath = "/Session/Create/";
                    });

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IAuthentication, Authentication>();

            services.Configure<Configuration.ApiConfiguration>(configuration);

            services.AddMvc();

            services.AddDbContext<BotContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
