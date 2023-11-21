// <copyright file="ObservabilityServiceCollectionExtensions.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Configuration
{
    using System.Diagnostics;
    using System.Diagnostics.Metrics;
    using OpenTelemetry.Metrics;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;

    /// <summary>
    /// Class ObservabilityServiceCollectionExtensions.
    /// </summary>
    public static class ObservabilityServiceCollectionExtensions
    {
        private const string ServiceName = "JCystems.MSLearn.Observability.WebApp";

        private const string Version = "1.0";

        private static readonly ActivitySource ActivitySource = new(
            ObservabilityServiceCollectionExtensions.ServiceName,
            ObservabilityServiceCollectionExtensions.Version);

        private static readonly Meter Meter = new(
            ObservabilityServiceCollectionExtensions.ServiceName,
            ObservabilityServiceCollectionExtensions.Version);

        /// <summary>
        /// Configures logging traces and metrics.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="host">The host.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection ConfigureObservability(
            this IServiceCollection services,
            IConfiguration configuration,
            ConfigureHostBuilder host)
        {
            services.AddLogging();

            var tracingOtlpEndpoint = configuration["OTLP_ENDPOINT_URL"];
            var oOpenTelemetryBuilder = services.AddOpenTelemetry();

            services.AddSingleton(ObservabilityServiceCollectionExtensions.ActivitySource);
            services.AddSingleton(ObservabilityServiceCollectionExtensions.Meter);

            // Configure OpenTelemetry Resources with the application name
            oOpenTelemetryBuilder
                .ConfigureResource(resource => resource
                    .AddService(serviceName: ServiceName));

            // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
            oOpenTelemetryBuilder
                .WithMetrics(metrics => metrics

                    // Metrics provider from OpenTelemetry
                    .AddAspNetCoreInstrumentation()
                    .AddMeter(Meter.Name)

                    // Metrics provides by ASP.NET Core in .NET 8
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddPrometheusExporter());

            // Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
            oOpenTelemetryBuilder.WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                tracing.AddSource(ActivitySource.Name);

                if (tracingOtlpEndpoint is null)
                {
                    tracing.AddConsoleExporter();
                }
                else
                {
                    tracing.AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
                    });
                }
            });

            return services;
        }
    }
}
