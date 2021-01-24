using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _5Quartz.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConsoleLog(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole(
#pragma warning disable 618
                    config => config.TimestampFormat = "[HH:mm:ss] "
#pragma warning restore 618
                );
            });
        }
    }
}
