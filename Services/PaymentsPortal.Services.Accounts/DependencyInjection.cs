using MassTransit;
using PaymentsPortal.Services.Accounts.Consumers;
using PaymentsPortal.Services.Accounts.Data;
using RabbitMQ.Client;

namespace PaymentsPortal.Services.Accounts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            return services;
        }

        private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configuration);
            services.AddDatabase(configuration);

            return services;
        }

        private static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                //Add consumers here
                x.AddConsumer<GetAccountsConsumer>();
                x.AddConsumer<GetAccountByIdConsumer>();
                x.AddConsumer<CreateAccountConsumer>();
                x.AddConsumer<SetAccountFreezeConsumer>();

                //Rabbit
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["EventBusConnection"], "/", h =>
                    {
                        if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                        {
                            h.Username(configuration["EventBusUserName"]!);
                        }

                        if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                        {
                            h.Password(configuration["EventBusPassword"]!);
                        }
                    });

                    cfg.ConfigureEndpoints(context);
                    cfg.ExchangeType = ExchangeType.Fanout;
                });
            });

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Add SQL Server
            var connectionString = configuration["ConnectionString"];
            services.AddSqlServer<AccountsDbContext>(connectionString);

            services.AddHostedService<DatabaseInitializer>();

            return services;
        }
    }
}
