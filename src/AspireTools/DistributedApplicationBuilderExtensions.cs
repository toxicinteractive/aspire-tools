using AspireTools.Codespaces;
using AspireTools.Health;
using AspireTools.Trust;
using AspireTools.Umbraco.Codespaces;

namespace AspireTools;

/// <summary>
/// Extensions for the App Host application builder.
/// </summary>
public static class DistributedApplicationBuilderExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        /// Adds opinionated services and options provided by the Toxic Aspire toolkit.
        /// </summary>
        public IDistributedApplicationBuilder WithAspireToolkitDefaults()
        {
            builder
                .WithCustomHttpsCertificates()
                .WithSecureHealthChecks()
                .WithCodespacesSupport()
                .WithUmbracoCodespacesSupport();

            return builder;
        }
    }
}
