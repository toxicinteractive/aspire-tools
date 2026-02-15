using Microsoft.AspNetCore.Builder;
using AspireTools.Health;

namespace AspireTools;

/// <summary>
/// Extensions for the individual host applications (not the App Host).
/// </summary>
public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        /// <summary>
        /// Configures a web application with opinionated settings.
        /// This should be called in every project that is orchestrated by the App Host.
        /// Should be called on both the <see cref="WebApplicationBuilder"/> and the <see cref="WebApplication"/>.
        /// </summary>
        public WebApplication WithAspireToolkitDefaults()
        {
            return app
                .MapHealthCheckEndpoints();
        }
    }
}
