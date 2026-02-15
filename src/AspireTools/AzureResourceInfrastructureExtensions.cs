using Aspire.Hosting.Azure;
using Azure.Provisioning.Primitives;

namespace AspireTools;

public static class AzureResourceInfrastructureExtensions
{
    extension(AzureResourceInfrastructure infrastructure)
    {
        /// <summary>
        /// Shortcut for getting a resource from <see cref="Azure.Provisioning.Infrastructure.GetProvisionableResources()"/>.
        /// Assumes only a single resource of the type exists.
        /// </summary>
        public TResource GetProvisionableResource<TResource>() where TResource : ProvisionableResource
        {
            return infrastructure
                .GetProvisionableResources()
                .OfType<TResource>()
                .Single();
        }
    }
}
