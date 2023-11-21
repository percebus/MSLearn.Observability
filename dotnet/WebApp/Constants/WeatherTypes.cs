// <copyright file="WeatherTypes.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Constants
{
    /// <summary>
    /// Kinds/Types of Weather static class.
    /// </summary>
    public static class WeatherTypes
    {
        /// <summary>
        /// Freezing.
        /// </summary>
        public const string Freezing = "Freezing";

        /// <summary>
        /// Bracing.
        /// </summary>
        public const string Bracing = "Bracing";

        /// <summary>
        /// Chilly.
        /// </summary>
        public const string Chilly = "Chilly";

        /// <summary>
        /// Cool.
        /// </summary>
        public const string Cool = "Cool";

        /// <summary>
        /// Mild.
        /// </summary>
        public const string Mild = "Mild";

        /// <summary>
        /// Warm.
        /// </summary>
        public const string Warm = "Warm";

        /// <summary>
        /// Balmy.
        /// </summary>
        public const string Balmy = "Balmy";

        /// <summary>
        /// Hot.
        /// </summary>
        public const string Hot = "Hot";

        /// <summary>
        /// Sweltering.
        /// </summary>
        public const string Sweltering = "Sweltering";

        /// <summary>
        /// Scorching.
        /// </summary>
        public const string Scorching = "Scorching";

        /// <summary>
        /// All types.
        /// </summary>
        public static readonly string[] All = new[]
        {
            Freezing, Bracing, Chilly, Cool, Mild, Warm, Balmy, Hot, Sweltering, Scorching,
        };
    }
}
