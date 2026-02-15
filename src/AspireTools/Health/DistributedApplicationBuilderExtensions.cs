#pragma warning disable ASPIREPROBES001

namespace AspireTools.Health;

/// <summary>
/// Extensions for the App Host application builder.
/// </summary>
internal static class DistributedApplicationBuilderExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        /// Configures internal health checks (probes) for project resources published as container apps.
        /// Internal health checks and probes are not exposed to the internet.
        /// This is part of the Toxic Aspire defaults.
        /// </summary>
        internal IDistributedApplicationBuilder WithSecureHealthChecks()
        {
            // projects
            foreach (var project in builder.Resources.OfType<ProjectResource>())
            {
                builder
                    .CreateResourceBuilder(project)
                    .WithInternalHealthProbes();
            }

            return builder;
        }
    }
}
