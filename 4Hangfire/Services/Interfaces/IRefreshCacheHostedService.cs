using System.Threading;
using System.Threading.Tasks;

namespace _4Hangfire.Services.Interfaces
{
    public interface IRefreshCacheHostedService
    {
        Task Refresh();
    }
}