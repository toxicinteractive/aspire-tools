namespace AspireTools.Umbraco;

public static class ResourceBuilderExtensions
{
    extension<TResource>(IResourceBuilder<TResource> builder) where TResource : IResourceWithEnvironment
    {
        /// <summary>
        /// Tags this resource as being an Umbraco application.
        /// This allows the application to be automatically configured for various settings such as Codespace support.
        /// </summary>
        public IResourceBuilder<TResource> IsUmbraco() =>
            builder.WithAnnotation(new IsUmbracoAnnotation());
    }
}
