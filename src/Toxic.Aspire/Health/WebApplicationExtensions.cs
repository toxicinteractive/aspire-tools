using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Toxic.Aspire.Health;

/// <summary>
/// Extensions for the individual host applications (not the App Host).
/// </summary>
internal static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        /// <summary>
        /// Adds the default endpoints for health and aliveness checks.
        /// Exposes the checks on an internal port instead of the public ingress port.
        /// This is part of the Toxic Aspire defaults.
        /// See <see cref="ResourceBuilderExtensions.WithInternalHealthProbes{TResource}(IResourceBuilder{TResource})"/>.
        /// </summary>
        internal WebApplication MapHealthCheckEndpoints()
        {
            var publishedHost = $"*:{Constants.HealthCheckPort}";

            // readyness, all health checks must pass for app to be considered ready to accept traffic after starting
            app
                .MapHealthChecks(Constants.ReadynessEndpointPath)
                .RequireHost("localhost", publishedHost)
                /*.AllowAnonymous()*/; // todo: needed?

            // aliveness, only health checks tagged with the "live" tag must pass for app to be considered alive
            app
                .MapHealthChecks(Constants.AlivenessEndpointPath, new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("live")
                })
                .RequireHost("localhost", publishedHost)
                /*.AllowAnonymous()*/; //TODO: confirm is this is needed in publish mode

            return app;
        }
    }
}
