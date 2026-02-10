using Microsoft.Extensions.DependencyInjection;
using Toxic.Aspire.Codespaces;

namespace Toxic.Aspire.Umbraco.Codespaces;

/// <summary>
/// Extensions for the App Host resource builder.
/// </summary>
internal static class ResourceBuilderExtensions
{
    extension<TResource>(IResourceBuilder<TResource> builder)
        where TResource : IResourceWithEndpoints, IResourceWithEnvironment
    {
        /// <summary>
        /// Sets the correct environment variables for an Umbraco application when running in a GitHub Codespace.
        /// Only targets resources that has the <see cref="IsUmbracoAnnotation"/> annotation.
        /// This is part of the Toxic Aspire defaults.
        /// </summary>
        internal IResourceBuilder<TResource> WithUmbracoCodespacesSupport()
        {
            if (!builder.ApplicationBuilder.ExecutionContext.IsRunMode ||
                !builder.ApplicationBuilder.ExecutionContext.IsCodespace ||
                !builder.Resource.HasAnnotationOfType<IsUmbracoAnnotation>())
            {
                return builder;
            }

            builder.OnResourceEndpointsAllocated((resource, @event, cancellationToken) =>
            {
                var codespaceUrlService = @event.Services.GetService<CodespaceUrlService>() ??
                    throw new InvalidOperationException($"You must call builder.{nameof(Aspire.DistributedApplicationBuilderExtensions.WithToxicDefaults)}() first.");

                var codespaceUrl = codespaceUrlService.GetCodespaceUrl(resource);

                builder.WithEnvironment("UMBRACO__CMS__WEBROUTING__UMBRACOAPPLICATIONURL", codespaceUrl);
                builder.WithEnvironment("UMBRACO__CMS__SECURITY__BACKOFFICEHOST", codespaceUrl);

                return Task.CompletedTask;
            });

            return builder;
        }
    }
}
