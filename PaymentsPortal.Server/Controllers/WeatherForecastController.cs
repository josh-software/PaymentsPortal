using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PaymentsPortal.Common.Contracts;
using PaymentsPortal.Server.Mappings;

namespace PaymentsPortal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRequestClient<GetWeatherForecastRequest> _getWeatherForecastClient;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IRequestClient<GetWeatherForecastRequest> getWeatherForecastClient,
            ILogger<WeatherForecastController> logger)
        {
            _getWeatherForecastClient = getWeatherForecastClient;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var response = await _getWeatherForecastClient
                .GetResponse<GetWeatherForecastResponse>(new GetWeatherForecastRequest());

            return response.Message.WeatherForecasts
                .Select(w => w.ToWeatherForecast());
        }
    }
}
