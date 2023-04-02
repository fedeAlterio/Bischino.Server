using Bischino.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bischino.Middlewares
{
    public class ApplicationLifetimeLoggerMiddleware
    {
        private readonly IApplicationContext _applicationContext;
        private readonly ILogger<ApplicationLifetimeLoggerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ApplicationLifetimeLoggerMiddleware(IApplicationContext applicationContext,
            ILogger<ApplicationLifetimeLoggerMiddleware> logger,
            RequestDelegate next)
        {
            _applicationContext = applicationContext;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logInfo = new { ELapsedTime = (DateTime.Now - _applicationContext.StartDate).TotalSeconds };
            var logJson = JsonSerializer.Serialize(logInfo);
            _logger.LogWarning(logJson);

            await _next(context).ConfigureAwait(false);
        }
    }
}
