using _2BackgroundService.Models;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace _2BackgroundService.Infrastructure
{
    public class OrderQueue : IOrderQueue
    {
        private readonly ConcurrentQueue<Order> _orders = new ConcurrentQueue<Order>();
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void EnQueue(Order order)
        {
            _orders.Enqueue(order);
            _signal.Release();
        }

        public async Task<Order> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _orders.TryDequeue(out var order);
            return order;
        }
    }
}
