using Bischino.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bischino.HostedServices
{
    public class ApplicationMonitoringHostedService : BackgroundService
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationMonitoringHostedService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _applicationContext.StartDate = DateTime.Now;          
        }
    }
}
