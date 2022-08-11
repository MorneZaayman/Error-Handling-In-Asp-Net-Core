using Microsoft.AspNetCore.Mvc;

namespace ErrorHandling.Controllers
{
    [ApiController]
    [Route("WeatherForecasts")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<WeatherForecast> WeatherForecasts = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = -5,
                Summary = "Freezing"
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = 5,
                Summary = "Chilly"
            },
            new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = 15,
                Summary = "Mild"
            },
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return WeatherForecasts;
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast weatherForecast)
        {
            if (weatherForecast.Date.Value.ToUniversalTime() < DateTime.UtcNow)
            {
                return Problem(title: "Invalid date", detail: "You cannot set a date in the past.", statusCode: StatusCodes.Status400BadRequest);
            }

            if (WeatherForecasts.Any(wf => wf.Date == weatherForecast.Date))
            {
                throw new InvalidOperationException($"A weather forecast has already been added for the date '{weatherForecast.Date}'.");
            }

            WeatherForecasts.Add(weatherForecast);

            return CreatedAtAction(nameof(Get), weatherForecast);
        }
    }
}