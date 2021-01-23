using System;
using _4Hangfire.Extensions;
using _4Hangfire.Services;
using _4Hangfire.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace _4Hangfire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsoleLog();
            services.AddControllers();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddHangfireServer(options =>
            {
                options.WorkerCount = 1;
            });

            services.AddTransient<IRefreshCacheHostedService, RefreshCacheHostedService>();
            services.AddTransient<IEmailNotificationHostedService, EmailNotificationHostedService>();
            services.AddTransient<IDbMigrationHostedService, DbMigrationHostedService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hangfire", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            RecurringJob.AddOrUpdate<IRefreshCacheHostedService>(s => s.Refresh(),
                Cron.Minutely,
                TimeZoneInfo.Local);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire v1"));
            }

            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
