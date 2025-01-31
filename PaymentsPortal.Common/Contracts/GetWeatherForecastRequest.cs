using PaymentsPortal.Common.DTOs;

namespace PaymentsPortal.Common.Contracts
{
    public class GetWeatherForecastRequest { }

    public class GetWeatherForecastResponse
    {
        public WeatherForecastDto[] WeatherForecasts { get; set; }
    }
}
