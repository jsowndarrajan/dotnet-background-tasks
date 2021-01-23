using System.Threading.Tasks;
using _4Hangfire.Models;
using _4Hangfire.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _4Hangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IEmailNotificationHostedService emailNotificationHostedService,
            [FromBody] Order order)
        {
            await Task.Delay(500).ContinueWith(task =>
            {
                _logger.LogInformation($"Order (Id: {order.ItemId}) is placed successfully for customer: {order.CustomerId}");
                BackgroundJob.Enqueue(() => emailNotificationHostedService.SendEmail(order));
            });
            return Ok();
        }
    }
}
