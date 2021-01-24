using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _1HostedService.Services
{
    public class RefreshCacheHostedService : IHostedService, IDisposable
    {
        private int _executionCount = 0;
        private readonly ILogger<RefreshCacheHostedService> _logger;
        private Timer _timer;

        public RefreshCacheHostedService(ILogger<RefreshCacheHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Refresh Cache Service is running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation("Refresh Cache Service is working. Count: {Count}", count);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(10000, cancellationToken).ContinueWith(task =>
                {
                    _logger.LogInformation("Refresh Cache Service has been shutdown gracefully.");

                }, cancellationToken);

                _timer?.Change(Timeout.Infinite, 0);
            }
            catch (Exception)
            {
                _logger.LogError("Refresh Cache Service has been KILLED.");
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}