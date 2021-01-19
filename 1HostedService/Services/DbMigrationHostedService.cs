using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace _1HostedService.Services
{
    public class DbMigrationHostedService : IHostedService
    {
        private readonly ILogger<DbMigrationHostedService> _logger;

        public DbMigrationHostedService(ILogger<DbMigrationHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Db migration service has started.");
            return DoWork(cancellationToken);
        }

        public virtual Task DoWork(CancellationToken cancellationToken)
        {
            Task.Delay(20000, cancellationToken)
                    .ContinueWith(task =>
                    {
                        if (task.IsCompletedSuccessfully)
                        {
                            _logger.LogInformation("Db migration has completed successfully");
                        }
                    }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Db migration service has shutdown successfully.");
            return Task.CompletedTask;
        }
    }
}
