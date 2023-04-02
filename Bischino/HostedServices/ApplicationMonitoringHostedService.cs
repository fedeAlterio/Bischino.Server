using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Bischino.HostedServices
{
    public class ApplicationMonitoringHostedService : BackgroundService
    {
        private readonly ILogger<ApplicationMonitoringHostedService> _logger;

        public ApplicationMonitoringHostedService(ILogger<ApplicationMonitoringHostedService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning("Application Started!");
            await Task.CompletedTask;
        }
    }
}
