using _5Quartz.Extensions;
using _5Quartz.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;

namespace _5Quartz
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

            services.AddQuartz(q =>
            {
                q.SchedulerId = "SchedulerDemo";
                q.UseMicrosoftDependencyInjectionJobFactory(options =>
                {
                    options.AllowDefaultConstructor = true;
                });

                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 1;
                });

                var jobKey = new JobKey("Refresh Cache", "Default");

                q.AddJob<RefreshCacheHostedService>(j => j
                    .WithIdentity(jobKey)
                );

                q.AddTrigger(t => t
                    .WithIdentity("Refresh Cache Trigger")
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(5)).RepeatForever())
                );
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
