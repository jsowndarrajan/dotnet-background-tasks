using _2BackgroundService.Models;
using System.Threading;
using System.Threading.Tasks;

namespace _2BackgroundService.Infrastructure
{
    public interface IOrderQueue
    {
        void EnQueue(Order order);

        Task<Order> DequeueAsync(CancellationToken cancellationToken);
    }
}
