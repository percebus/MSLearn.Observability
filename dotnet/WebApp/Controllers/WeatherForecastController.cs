// <copyright file="WeatherForecastController.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Controllers
{
    using System.Diagnostics;
    using System.Diagnostics.Metrics;
    using JCystems.MSLearn.Observability.WebApp.Constants;
    using JCystems.MSLearn.Observability.WebApp.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Weather Forecast Controller class.
    /// </summary>
    [Route("api/weather/forecasts")]
    public class WeatherForecastController : ObservableControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance for <see cref="GreetingsController"/>.</param>
        /// <param name="activitySource"><see cref="ActivitySource"/> instance.</param>
        /// <param name="meter"><see cref="Meter"/> instance.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ActivitySource activitySource, Meter meter)
            : base(logger, activitySource, meter)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets WeatherForecast.
        /// </summary>
        /// <returns>Collection of WeatherForecast.</returns>
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<WeatherForecast>> Find()
        {
            using (var activity = this.ActivitySource.StartActivity())
            {
                this.Logger.LogInformation("WeatherForecast/Find request");

                this.Logger.LogDebug("Randomly generating forecasts.");

                return Enumerable
                    .Range(1, 5)
                    .Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = WeatherTypes.All[Random.Shared.Next(WeatherTypes.All.Length)],
                    })
                    .ToArray();
            }
        }
    }
}
