// <copyright file="GreetingsController.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Controllers
{
    using System.Diagnostics;
    using System.Diagnostics.Metrics;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Greetings Controller.
    /// </summary>
    public class GreetingsController : ObservableControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreetingsController"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance for <see cref="GreetingsController"/>.</param>
        /// <param name="activitySource"><see cref="ActivitySource"/> instance.</param>
        /// <param name="meter"><see cref="Meter"/> instance.</param>
        public GreetingsController(ILogger<GreetingsController> logger, ActivitySource activitySource, Meter meter)
            : base(logger, activitySource, meter)
        {
            this.GreetingsCount = this.Meter.CreateCounter<int>("greetings.count", description: "Counts the number of greetings");
        }

        private Counter<int> GreetingsCount { get; set; }

        private string DefaultGreeting { get; set; } = "Hello World!";

        /// <summary>
        /// Posts a new Greeting.
        /// </summary>
        /// <param name="greeting"><see cref="string"/> greeting.</param>
        /// <returns>An asynchronous <see cref="Task"/> of an <see cref="ActionResult"/> of a <see cref="string"/> greeting message.</returns>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult<string> Post(string? greeting = null)
        {
            using (var activity = this.ActivitySource.StartActivity())
            {
                activity?.SetTag("greeting", greeting);

                this.Logger.LogInformation("Posting new greeting: '{greeting}'", greeting);
                if (string.IsNullOrWhiteSpace(greeting))
                {
                    this.Logger.LogDebug("Greeting was empty, defaulting to '{greeting}'", this.DefaultGreeting);
                    greeting = this.DefaultGreeting;
                }

                activity?.SetTag("greeting", greeting);

                this.GreetingsCount.Add(1);

                return greeting;
            }
        }
    }
}
