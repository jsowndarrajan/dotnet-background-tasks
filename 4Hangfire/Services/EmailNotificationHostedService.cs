using _4Hangfire.Models;
using _4Hangfire.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace _4Hangfire.Services
{
    public class EmailNotificationHostedService : IEmailNotificationHostedService
    {
        private readonly ILogger<EmailNotificationHostedService> _logger;

        public EmailNotificationHostedService(ILogger<EmailNotificationHostedService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmail(Order order)
        {
            try
            {
                await Task.Delay(5000).ContinueWith(task =>
                {
                    _logger.LogInformation($"Email notification has sent to customer: {order.CustomerId} for order: {order.ItemId}");
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while sending notification to customer : {order.CustomerId}");
            }
        }
    }
}
