using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace _5Quartz.Services
{
    public class RefreshCacheHostedService : IJob
    {
        private readonly ILogger<RefreshCacheHostedService> _logger;

        public RefreshCacheHostedService(ILogger<RefreshCacheHostedService> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(5000).ContinueWith(task =>
            {
                _logger.LogInformation("Refresh Cache Service is working.");
            });
        }
    }
}