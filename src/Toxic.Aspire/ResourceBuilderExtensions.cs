using Toxic.Aspire.Codespaces;

namespace Toxic.Aspire;

/// <summary>
/// Extensions for the App Host resource builder.
/// </summary>
public static class ResourceBuilderExtensions
{
    extension<TResource>(IResourceBuilder<TResource> builder) where TResource : IResource
    {
        /// <summary>
        /// Exclude the resource from the Aspire provisioning manifest in Production publish mode.
        /// </summary>
        public IResourceBuilder<TResource> ExcludeFromProductionManifest() =>
            builder.ExcludeFromManifest(["Production"]);

        /// <summary>
        /// Exclude the resource from the Aspire provisioning manifest in Staging publish mode.
        /// </summary>
        public IResourceBuilder<TResource> ExcludeFromStageManifest() =>
            builder.ExcludeFromManifest(["Staging"]);

        /// <summary>
        /// Exclude the resource from the Aspire provisioning manifest in Development publish mode.
        /// </summary>
        public IResourceBuilder<TResource> ExcludeFromDevelopmentManifest() =>
            builder.ExcludeFromManifest(["Development"]);

        /// <summary>
        /// Excludes the resource from the Aspire provisioning manifest in publish mode if an environment name is matched.
        /// Use this to not provision certain resources to Azure in development mode for example.
        /// </summary>
        public IResourceBuilder<TResource> ExcludeFromManifest(IEnumerable<string> environmentNames)
        {
            if (builder.ApplicationBuilder.ExecutionContext.IsPublishMode &&
                environmentNames.Any(x => string.Equals(
                    x,
                    builder.ApplicationBuilder.Environment.EnvironmentName,
                    StringComparison.OrdinalIgnoreCase)))
            {
                builder.ExcludeFromManifest();
            }

            return builder;
        }
    }
}
