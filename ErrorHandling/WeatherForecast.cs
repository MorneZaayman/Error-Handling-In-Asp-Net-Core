using System.ComponentModel.DataAnnotations;

namespace ErrorHandling
{
    public class WeatherForecast
    {
        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public int? TemperatureC { get; set; }

        public string? Summary { get; set; }
    }
}