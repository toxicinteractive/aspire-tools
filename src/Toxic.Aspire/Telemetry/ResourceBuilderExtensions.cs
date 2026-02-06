using Aspire.Hosting.Azure;
using Azure.Provisioning.Expressions;
using Microsoft.Extensions.Hosting;

namespace Toxic.Aspire.Telemetry;

public static class ResourceBuilderExtensions
{
    extension<TResource>(IResourceBuilder<TResource> builder) where TResource : IResourceWithEnvironment
    {
        /// <summary>
        /// Configures the Open Telemetry exporter to send telemetry to the New Relic OTLP endpoint.
        /// The endpoint URL defaults to "https://otlp.eu01.nr-data.net" but can be overridden with the configuration variable "OTEL_EXPORTER_OTLP_ENDPOINT".
        /// This will automatically forward traces and metrics but to forward logs you need to configure your logging provider. For Serilog, use the official OpenTelemetry sink.
        /// </summary>
        /// <param name="apiKey">A Key Vault reference to a New Relic API key.</param>
        /// <param name="entityName">The New Relic entity name for the service, or the resource name if null. The current environment name will be added to the name.</param>
        /// <param name="includeInDevelopmentEnvironment">Whether to send telemetry in the development environment.</param>
        public IResourceBuilder<TResource> WithNewRelicTelemetry(
            IAzureKeyVaultSecretReference apiKey,
            string? entityName = null,
            bool includeInDevelopmentEnvironment = false) =>
            WithNewRelicTelemetry(builder, ReferenceExpression.Create($"api-key={apiKey}"), entityName, includeInDevelopmentEnvironment);

        /// <summary>
        /// Configures the Open Telemetry exporter to send telemetry to the New Relic OTLP endpoint.
        /// The endpoint URL defaults to "https://otlp.eu01.nr-data.net" but can be overridden with the configuration variable "OTEL_EXPORTER_OTLP_ENDPOINT".
        /// This will automatically forward traces and metrics but to forward logs you need to configure your logging provider. For Serilog, use the official OpenTelemetry sink.
        /// </summary>
        /// <param name="apiKey">A Parameter reference to a New Relic API key.</param>
        /// <param name="entityName">The New Relic entity name for the service, or the resource name if null. The current environment name will be added to the name.</param>
        /// <param name="includeInDevelopmentEnvironment">Whether to send telemetry in the development environment.</param>
        public IResourceBuilder<TResource> WithNewRelicTelemetry(
            ParameterResource apiKey,
            string? entityName = null,
            bool includeInDevelopmentEnvironment = false) =>
            WithNewRelicTelemetry(builder, ReferenceExpression.Create($"api-key={apiKey}"), entityName, includeInDevelopmentEnvironment);

        /// <summary>
        /// Configures the Open Telemetry exporter to send telemetry to the New Relic OTLP endpoint.
        /// The endpoint URL defaults to "https://otlp.eu01.nr-data.net" but can be overridden with the configuration variable "OTEL_EXPORTER_OTLP_ENDPOINT".
        /// This will automatically forward traces and metrics but to forward logs you need to configure your logging provider. For Serilog, use the official OpenTelemetry sink.
        /// </summary>
        /// <param name="apiKey">A New Relic API key.</param>
        /// <param name="entityName">The New Relic entity name for the service, or the resource name if null. The current environment name will be added to the name.</param>
        /// <param name="includeInDevelopmentEnvironment">Whether to send telemetry in the development environment.</param>
        public IResourceBuilder<TResource> WithNewRelicTelemetry(
            string apiKey,
            string? entityName = null,
            bool includeInDevelopmentEnvironment = false) =>
            WithNewRelicTelemetry(builder, ReferenceExpression.Create($"api-key={apiKey}"), entityName, includeInDevelopmentEnvironment);

        /// <summary>
        /// Configures the Open Telemetry exporter to send telemetry to the New Relic OTLP endpoint.
        /// The endpoint URL defaults to "https://otlp.eu01.nr-data.net" but can be overridden with the configuration variable "OTEL_EXPORTER_OTLP_ENDPOINT".
        /// This will automatically forward traces and metrics but to forward logs you need to configure your logging provider. For Serilog, use the official OpenTelemetry sink.
        /// </summary>
        /// <param name="apiKeyExpression">A reference expression to a New Relic API key.</param>
        /// <param name="entityName">The New Relic entity name for the service, or the resource name if null. The current environment name will be added to the name.</param>
        /// <param name="includeInDevelopmentEnvironment">Whether to send telemetry in the development environment.</param>
        public IResourceBuilder<TResource> WithNewRelicTelemetry(
            ReferenceExpression apiKeyExpression,
            string? entityName = null,
            bool includeInDevelopmentEnvironment = false)
        {
            if (!includeInDevelopmentEnvironment &&
                builder.ApplicationBuilder.Environment.IsDevelopment())
            {
                return builder;
            }

            var endpoint =
                builder.ApplicationBuilder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ??
                "https://otlp.eu01.nr-data.net";

            entityName = $"{entityName ?? builder.Resource.Name} ({builder.ApplicationBuilder.Environment.EnvironmentName})";

            return builder
                .WithEnvironment("OTEL_EXPORTER_OTLP_ENDPOINT", endpoint)
                .WithEnvironment("OTEL_SERVICE_NAME", entityName)
                .WithEnvironment("OTEL_EXPORTER_OTLP_HEADERS", apiKeyExpression);
        }
    }
}
