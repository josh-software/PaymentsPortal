using PaymentsPortal.Common.DTOs;

namespace PaymentsPortal.Server.Mappings
{
    public static class WeatherForecastMappingExtensions
    {
        public static WeatherForecast ToWeatherForecast(this WeatherForecastDto weatherForecastDto)
        {
            return new WeatherForecast
            {
                Date = weatherForecastDto.Date,
                TemperatureC = weatherForecastDto.TemperatureC,
                Summary = weatherForecastDto.Summary
            };
        }
    }
}
