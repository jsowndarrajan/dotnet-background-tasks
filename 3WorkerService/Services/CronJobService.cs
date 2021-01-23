using Cronos;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace _3WorkerService.Services
{
    public abstract class CronJobService: BackgroundService
    {
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;

        private Timer _timer;

        protected CronJobService(CronExpression expression, TimeZoneInfo timeZoneInfo)
        {
            _expression = expression;
            _timeZoneInfo = timeZoneInfo;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)
                {
                    await ExecuteAsync(cancellationToken);
                }
                _timer = new Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteAsync(cancellationToken);
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

        protected abstract Task DoWork(CancellationToken cancellationToken);

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
