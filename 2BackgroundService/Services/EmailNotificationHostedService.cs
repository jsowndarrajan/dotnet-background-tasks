using _2BackgroundService.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _2BackgroundService.Services
{
    public class EmailNotificationHostedService : BackgroundService
    {
        private readonly ILogger<EmailNotificationHostedService> _logger;
        private readonly IOrderQueue _queue;

        public EmailNotificationHostedService(ILogger<EmailNotificationHostedService> logger,
            IOrderQueue queue)
        {
            _logger = logger;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //await Task.Delay(20000, cancellationToken);
            _logger.LogInformation("Email notification service is started");
            await BackgroundProcessing(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var order = await _queue.DequeueAsync(cancellationToken);
                try
                {
                    await Task.Delay(5000, cancellationToken).ContinueWith(task =>
                    {
                        _logger.LogInformation($"Email notification has sent to customer: {order.CustomerId} for order: {order.ItemId}");
                    }, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while sending notification to customer : {order.CustomerId}");
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Email notification service is stopping.");
            await base.StopAsync(cancellationToken);
        }
    }
}
