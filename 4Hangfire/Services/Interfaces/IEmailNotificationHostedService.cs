using _4Hangfire.Models;
using System.Threading.Tasks;

namespace _4Hangfire.Services.Interfaces
{
    public interface IEmailNotificationHostedService
    {
        Task SendEmail(Order order);
    }
}