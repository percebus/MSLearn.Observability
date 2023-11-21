// <copyright file="ServicesConfigurator.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
namespace JCystems.MSLearn.Observability.WebApp.Configuration
{
    /// <summary>
    /// Class ServiceConfigurator.
    /// </summary>
    public static class ServicesConfigurator
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="host">The host.</param>
        /// <returns>A IServiceCollection.</returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host)
        {
            // appsettings
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            configuration.AddEnvironmentVariables();

            // appsettings Class value holder
            services.Configure<AppSettings>(c =>
            {
                configuration.GetSection(AppSettings.Key).Bind(c);
                c.Validate();
            });

            // Observability w/ OpenTelemetry
            services.ConfigureObservability(configuration, host);

            // Scrutor
            services.Scan(s => s
                .FromAssemblyOf<Program>()
                .AddClasses()
                .AsMatchingInterface());

            return services;
        }
    }
}
