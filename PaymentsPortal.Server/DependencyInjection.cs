using MassTransit;
using PaymentsPortal.Common.Contracts;
using RabbitMQ.Client;

namespace PaymentsPortal.Server
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

            return services;
        }

        private static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                //Add request clients here
                x.AddRequestClient<GetAccountsRequest>();
                x.AddRequestClient<GetAccountByIdRequest>();
                x.AddRequestClient<CreateAccountRequest>();
                x.AddRequestClient<SetAccountFreezeRequest>();

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
    }
}
