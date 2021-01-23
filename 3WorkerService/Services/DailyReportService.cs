using Cronos;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace _3WorkerService.Services
{

    /*                                  Allowed values      Allowed special characters             Comment

    ┌───────────── second(optional)        0-59                   * , - /                      
    │ ┌───────────── minute                0-59                   * , - /                      
    │ │ ┌───────────── hour                0-23                   * , - /                      
    │ │ │ ┌───────────── day of month      1-31                   * , - / L W ?                
    │ │ │ │ ┌───────────── month           1-12 or JAN-DEC        * , - /                      
    │ │ │ │ │ ┌───────────── day of week   0-6  or SUN-SAT        * , - / # L ?             Both 0 and 7 means SUN
    │ │ │ │ │ │
    * * * * * *
    
    */
    public class DailyReportService: CronJobService
    {
        private readonly ILogger<DailyReportService> _logger;

        public DailyReportService(ILogger<DailyReportService> logger)
            : base(CronExpression.Parse("* * * * *"),
                TimeZoneInfo.Local)
        {
            _logger = logger;
        }

        protected override async Task DoWork(CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken).ContinueWith(task =>
            {
                _logger.LogInformation($"Daily report has sent to stack holders");
            }, cancellationToken);
        }
    }
}
