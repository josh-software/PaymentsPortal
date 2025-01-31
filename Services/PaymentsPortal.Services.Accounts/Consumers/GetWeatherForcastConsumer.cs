using MassTransit;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Common.DTOs;

namespace PaymentsPortal.Services.Accounts.Consumers
{
    public class GetWeatherForcastConsumer : IConsumer<GetWeatherForecastRequest>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public GetWeatherForcastConsumer() { }

        public async Task Consume(ConsumeContext<GetWeatherForecastRequest> consumer)
        {
            var forcasts = Enumerable.Range(1, 5).Select(
                index => new WeatherForecastDto
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();

            await consumer.RespondAsync(
                new GetWeatherForecastResponse
                {
                    WeatherForecasts = forcasts
                }
            );
        }
    }
}
