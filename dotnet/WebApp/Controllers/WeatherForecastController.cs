// <copyright file="WeatherForecastController.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Controllers
{
    using JCystems.MSLearn.Observability.WebApp.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Weather Forecast Controller class.
    /// </summary>
    [ApiController]
    [Route("weather/forecast")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.Logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private ILogger<WeatherForecastController> Logger { get; set; }

        /// <summary>
        /// Gets WeatherForecast.
        /// </summary>
        /// <returns>Collection of WeatherForecast.</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Find()
        {
            this.Logger.LogInformation("WeatherForecast/Find request");

            this.Logger.LogDebug("Randomly generating forecasts.");

            return Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                })
                .ToArray();
        }
    }
}
