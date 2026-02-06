using Microsoft.Extensions.DependencyInjection;
using Toxic.Aspire.Umbraco;

namespace Toxic.Aspire.Codespaces;

/// <summary>
/// Extensions for the App Host application builder.
/// </summary>
internal static class DistributedApplicationBuilderExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        /// Adds services to support GitHub Codespaces support.
        /// This is part of the Toxic Aspire defaults.
        /// </summary>
        internal IDistributedApplicationBuilder WithCodespacesSupport()
        {
            builder.Services
                .ConfigureOptions<CodespacesOptionsConfigurator>()
                .AddSingleton<CodespaceUrlService>();

            builder.WithUmbracoCodespacesSupport();

            return builder;
        }

        /// <summary>
        /// Adds GitHub Codespaces support for Umbraco applications.
        /// </summary>
        internal IDistributedApplicationBuilder WithUmbracoCodespacesSupport()
        {
            // projects
            foreach (var q in builder.Resources
                .OfType<ProjectResource>()
                .Where(x => x.HasAnnotationOfType<IsUmbracoAnnotation>()))
            {
                builder
                    .CreateResourceBuilder(q)
                    .WithUmbracoCodespacesSupport();
            }

            // containers
            foreach (var q in builder.Resources
                .OfType<ContainerResource>()
                .Where(x => x.HasAnnotationOfType<IsUmbracoAnnotation>()))
            {
                builder
                    .CreateResourceBuilder(q)
                    .WithUmbracoCodespacesSupport();
            }

            return builder;
        }
    }
}
