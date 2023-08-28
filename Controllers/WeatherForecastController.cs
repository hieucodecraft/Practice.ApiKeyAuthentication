using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.ApiKeyAuthentication;
using Practice.ApiKeyAuthentication.Controllers;
using Practice.ApiKeyAuthentication.Services;
using Practice.ApiKeyAuthentication.Ultils;

namespace Practice.ApiKeyAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IApiKeyValidation _apiKeyValidation;

        public WeatherForecastController(IApiKeyValidation apiKeyValidation,
            ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _apiKeyValidation = apiKeyValidation;
        }

        [HttpGet]
        [Authorize(Policy = "ApiKeyPolicy")]
        public IActionResult AuthenticateViaQueryParam()
        {
            var t = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(t);
        }
    }
}
