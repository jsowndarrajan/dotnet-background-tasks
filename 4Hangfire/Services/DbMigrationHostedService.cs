using _4Hangfire.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace _4Hangfire.Services
{
    public class DbMigrationHostedService : IDbMigrationHostedService
    {
        private readonly ILogger<DbMigrationHostedService> _logger;

        public DbMigrationHostedService(ILogger<DbMigrationHostedService> logger)
        {
            _logger = logger;
        }

        public virtual async Task Migrate()
        {
            await Task.Delay(20000)
                .ContinueWith(task =>
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        _logger.LogInformation("Db migration has completed successfully");
                    }
                });
        }
    }
}
