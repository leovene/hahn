using Hahn.Jobs.Interfaces;
using Hangfire;

namespace Hahn.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;

        public Worker(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<Worker> logger)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cronExpression = _configuration.GetValue<string>("Hangfire:UpdateMuseumDataCron");
            if (string.IsNullOrEmpty(cronExpression))
            {
                _logger.LogError("The CRON expression for update-museum-data is not configured.");
                throw new InvalidOperationException("The CRON expression for update-museum-data is not configured.");
            }

            RecurringJob.AddOrUpdate<IUpdateMuseumDataJob>(
                "UpdateMuseumData",
                job => job.ExecuteAsync(),
                cronExpression
            );

            using (var scope = _serviceProvider.CreateScope())
            {
                var job = scope.ServiceProvider.GetRequiredService<IUpdateMuseumDataJob>();
                await job.ExecuteAsync();
            }

            await Task.CompletedTask;
        }
    }
}
