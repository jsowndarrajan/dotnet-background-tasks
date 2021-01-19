using System;
using _1HostedService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _1HostedService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                })
                .ConfigureServices(services =>
                {
                    services.Configure<HostOptions>(option =>
                    {
                        option.ShutdownTimeout = TimeSpan.FromSeconds(20);
                    });

                    services.AddHostedService<RefreshCacheHostedService>();
                });
    }
}


