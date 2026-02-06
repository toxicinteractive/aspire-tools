using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;

namespace Toxic.Aspire.NamingConventions;

public static class ResourceBuilderExtensions
{
    extension<TResource>(IResourceBuilder<TResource> builder) where TResource : IResource
    {
        /// <summary>
        /// Set a specific name for this resource to distinguish it from other similar resources.
        /// Setting "cms" for a container app resource will result in the following name: "ca-cms-...".
        /// Can also override the entire resource name and ignore name resolvers.
        /// </summary>
        /// <param name="name">A specific resource workload name distinguisher.</param>
        /// <param name="useAsResourceName">If true will ignore all registered name resolvers and set the resource name to the name specified.</param>
        /// <returns></returns>
        public IResourceBuilder<TResource> WithWorkloadName(string name, bool useAsResourceName = false)
        {
            // there's no connection between a resource builder and the infrastructure resolver so we need to
            // register a mediator object that holds the resource name and the workload name
            builder
                .ApplicationBuilder
                .Services
                .AddKeyedSingleton<ResourceWorkloadNameAssociation>(
                    builder.Resource.Name, new ResourceWorkloadNameAssociation
                    {
                        ResourceType = typeof(TResource),
                        ResourceName = builder.Resource.Name,
                        WorkloadName = name,
                        IgnoreNameResolvers = useAsResourceName
                    });

            return builder;
        }
    }
}
