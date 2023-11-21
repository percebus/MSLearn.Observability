// <copyright file="ObservableControllerBase.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Controllers
{
    using System.Diagnostics;
    using System.Diagnostics.Metrics;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Monitored Controller Base class.
    /// </summary>
    public abstract class ObservableControllerBase : ApiControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableControllerBase"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance for <see cref="ControllerBase"/>.</param>
        /// <param name="activitySource"><see cref="ActivitySource"/> instance.</param>
        /// <param name="meter"><see cref="Meter"/> instance.</param>
        public ObservableControllerBase(ILogger<ControllerBase> logger, ActivitySource activitySource, Meter meter)
        {
            this.Logger = logger;
            this.ActivitySource = activitySource;
            this.Meter = meter;
        }

        /// <summary>
        /// Gets or sets Logger for <see cref="ControllerBase"/> class.
        /// </summary>
        protected ILogger<ControllerBase> Logger { get; set; }

        /// <summary>
        /// Gets or Sets Open Telemetry Activity.
        /// </summary>
        protected ActivitySource ActivitySource { get; set; }

        /// <summary>
        /// Gets or Sets Open Telemetry Meter.
        /// </summary>
        protected Meter Meter { get; set; }
    }
}
