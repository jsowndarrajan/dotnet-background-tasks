using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _1HostedService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConsoleLog(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole(
                    config => config.TimestampFormat = "[HH:mm:ss]"
                );
            });
        }
    }
}