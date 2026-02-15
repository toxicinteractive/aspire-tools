#pragma warning disable ASPIREPROBES001

namespace AspireTools.Health;

/// <summary>
/// Extensions for the App Host resource builder.
/// </summary>
internal static class ResourceBuilderExtensions
{
    extension<TResource>(IResourceBuilder<TResource> builder) where TResource : IResourceWithEndpoints, IResourceWithProbes
    {
        /// <summary>
        /// Adds HTTP health probes on a container app on port 8081 allowing for internal readyness and aliveness checks.
        /// Internal health probes are not exposed to the internet. In run mode this uses the main https endpoint instead.
        /// This is part of the Toxic Aspire defaults.
        /// </summary>
        internal IResourceBuilder<TResource> WithInternalHealthProbes()
        {
            // use main https endpoint in run mode
            string? endpointName = null;

            if (builder.ApplicationBuilder.ExecutionContext.IsPublishMode)
            {
                // use custom internal endpoint in publish mode
                endpointName = Constants.HealthEndpointName;

                builder
                    .WithHttpEndpoint(name: Constants.HealthEndpointName, targetPort: Constants.HealthCheckPort, isProxied: false)
                    .WithUrlForEndpoint(Constants.HealthEndpointName, x => x.DisplayLocation = UrlDisplayLocation.DetailsOnly);
            }

            return builder
                .WithHttpProbe(ProbeType.Liveness, Constants.AlivenessEndpointPath, periodSeconds: 10, endpointName: endpointName)
                .WithHttpProbe(ProbeType.Readiness, Constants.ReadynessEndpointPath, periodSeconds: 10, endpointName: endpointName);
        }
    }
}
