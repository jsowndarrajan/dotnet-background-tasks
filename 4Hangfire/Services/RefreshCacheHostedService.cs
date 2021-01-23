using _4Hangfire.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace _4Hangfire.Services
{
    public class RefreshCacheHostedService : IRefreshCacheHostedService
    {
        private readonly ILogger<RefreshCacheHostedService> _logger;

        public RefreshCacheHostedService(ILogger<RefreshCacheHostedService> logger)
        {
            _logger = logger;
        }

        public async Task Refresh()
        {
            await Task.Delay(5000).ContinueWith(task =>
            {
                _logger.LogInformation("Refresh Cache Service is working.");
            });
        }
    }
}