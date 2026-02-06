using Aspire.Hosting.Azure;
using Azure.Core;
using Azure.Provisioning.AppContainers;
using Azure.Provisioning.ContainerRegistry;
using Azure.Provisioning.KeyVault;
using Azure.Provisioning.OperationalInsights;
using Azure.Provisioning.Sql;
using Azure.Provisioning.Storage;
using Microsoft.Extensions.DependencyInjection;
using Toxic.Aspire.NamingConventions.NameResolvers;
using Toxic.Aspire.NamingConventions.NameResolvers.Resources;

namespace Toxic.Aspire.NamingConventions;

public static class DistributedApplicationBuilderExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        /// Use the Azure resource naming convention system.
        /// By default produces the following resource name structure: "type-workload-project-environment-region", e.g. "ca-cms-hgfse-prod-swc".
        /// Use the <see cref="IResourceNameResolver{T}"/> to override the naming resolution for specific resource types.
        /// </summary>
        /// <param name="projectName">Short project name identifier, e.g. "hgfse".</param>
        public IDistributedApplicationBuilder WithAzureNamingConvention(string projectName)
        {
            // environment resolver
            builder.Services.AddSingleton<IEnvironmentNameResolver, DefaultEnvironmentNameResolver>();

            // default resource resolvers
            builder.Services.AddSingleton<IResourceNameResolver<ContainerApp>, DefaultResourceNameResolver<ContainerApp>>();
            builder.Services.AddSingleton<IResourceNameResolver<KeyVaultService>, DefaultResourceNameResolver<KeyVaultService>>();
            builder.Services.AddSingleton<IResourceNameResolver<ContainerRegistryService>, DefaultResourceNameResolver<ContainerRegistryService>>();
            builder.Services.AddSingleton<IResourceNameResolver<OperationalInsightsWorkspace>, DefaultResourceNameResolver<OperationalInsightsWorkspace>>();
            builder.Services.AddSingleton<IResourceNameResolver<SqlServer>, DefaultResourceNameResolver<SqlServer>>();
            builder.Services.AddSingleton<IResourceNameResolver<StorageAccount>, DefaultResourceNameResolver<StorageAccount>>();

            // specific resource resolver overrides
            builder.Services.AddSingleton<IResourceNameResolver<ContainerAppManagedEnvironment>, ContainerAppManagedEnvironmentNameResolver>();
            builder.Services.AddSingleton<IResourceNameResolver<SqlDatabase>, SqlDatabaseNameResolver>();

            builder.Services.Configure<AzureProvisioningOptions>(options =>
            {
                options
                    .ProvisioningBuildOptions
                    .InfrastructureResolvers
                    .Insert(0, new NamingInfrastructureResolver(
                        projectName,
                        builder.Environment.EnvironmentName,
                        GetDefaultLocationFromConfig(builder),
                        builder.Services));
            });

            return builder;
        }

        /// <summary>
        /// Gets the specified default location for all resources from the Azure:Location setting.
        /// These settings must be specified for all applicable environment configurations.
        /// </summary>
        private AzureLocation GetDefaultLocationFromConfig()
        {
            var locationName = builder.Configuration["Azure:Location"] ??
                throw new InvalidOperationException("App setting Azure:Location missing");

            var location = new AzureLocation(locationName);

            if (string.IsNullOrWhiteSpace(location.DisplayName))
            {
                throw new InvalidOperationException("Invalid Azure:Location setting");
            }

            return location;
        }
    }
}
