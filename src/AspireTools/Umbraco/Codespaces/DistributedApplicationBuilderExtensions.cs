namespace AspireTools.Umbraco.Codespaces;

/// <summary>
/// Extensions for the App Host application builder.
/// </summary>
internal static class DistributedApplicationBuilderExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
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
