using System.Threading;
using System.Threading.Tasks;

namespace _4Hangfire.Services.Interfaces
{
    public interface IDbMigrationHostedService
    {
        Task Migrate();
    }
}