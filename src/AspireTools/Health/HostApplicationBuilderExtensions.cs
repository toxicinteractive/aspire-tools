using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace AspireTools.Health;

/// <summary>
/// Extensions for the individual host applications (not the App Host).
/// </summary>
internal static class HostApplicationBuilderExtensions
{
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        /// Adds default readiness and liveness checks to ensure app is responsive.
        /// This is part of the Toxic Aspire defaults.
        /// </summary>
        internal TBuilder AddDefaultHealthChecks()
        {
            builder.Services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), [Constants.AlivenessTag]);

            return builder;
        }
    }
}
