namespace PaymentsPortal.Services.Accounts.Data
{
    public class DatabaseInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var host = scope.ServiceProvider;
            try
            {
                await host.CreateDbIfNotExistsAsync();
                _logger.LogInformation("Database initialization completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database initialization failed.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
