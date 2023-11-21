// <copyright file="WeatherForecast.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Models
{
    /// <summary>
    /// Weather Forecast class.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets Date.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets Temperature in Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets Temperature in Fahrenheit.
        /// </summary>
        public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets Summary.
        /// </summary>
        public string? Summary { get; set; }
    }
}
