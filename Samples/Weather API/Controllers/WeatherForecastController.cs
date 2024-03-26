using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Witivio.WeatherApi.Controllers
{
    /// <summary>
    /// Controller for managing weather forecasts.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Provides API operations for weather forecasts.")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">The logger used by this controller.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the weather forecast for the next five days.
        /// </summary>
        /// <returns>A list of weather forecasts.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        [SwaggerOperation(
            Summary = "Fetches weather forecasts.",
            Description = "Fetches a list of weather forecasts for the next five days.",
            OperationId = "WeatherForecast.Get",
            Tags = new[] { "WeatherForecast" }
        )]
        [SwaggerResponse(200, "The weather forecasts were successfully retrieved.", typeof(IEnumerable<WeatherForecast>))]
        [SwaggerResponse(500, "An error occurred while fetching the weather forecasts.")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}