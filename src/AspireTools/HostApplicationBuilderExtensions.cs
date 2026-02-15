using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspireTools.Health;
using AspireTools.Telemetry;

namespace AspireTools;

/// <summary>
/// Extensions for the individual host applications (not the App Host).
/// </summary>
public static class HostApplicationBuilderExtensions
{
    extension<TBuilder>(TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        /// <summary>
        /// Configures default settings for health, telemetry, service discovery, resiliency etc.
        /// This should be called in every project that is orchestrated by the App Host.
        /// Should be called on both the <see cref="Microsoft.AspNetCore.Builder.WebApplicationBuilder"/> and the <see cref="Microsoft.AspNetCore.Builder.WebApplication"/>.
        /// </summary>
        public TBuilder WithAspireToolkitDefaults()
        {
            builder
                .ConfigureOpenTelemetry()
                .AddDefaultHealthChecks();

            builder.Services
                .AddServiceDiscovery();

            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                http.AddStandardResilienceHandler();
                http.AddServiceDiscovery();
            });

            return builder;
        }
    }
}
