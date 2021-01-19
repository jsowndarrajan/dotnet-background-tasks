using _2BackgroundService.Infrastructure;
using _2BackgroundService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace _2BackgroundService.Controllers
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
        public async Task<IActionResult> Post([FromServices] IOrderQueue orderQueue, [FromBody] Order order)
        {
            await Task.Delay(500).ContinueWith(task =>
            {
                _logger.LogInformation($"Order (Id: {order.ItemId}) is placed successfully for customer: {order.CustomerId}");
                orderQueue.EnQueue(order);
            });
            return Ok();
        }
    }
}
