using Azure.Provisioning.AppContainers;

namespace AspireTools.NamingConventions.NameResolvers.Resources;

public class ContainerAppManagedEnvironmentNameResolver : DefaultResourceNameResolver<ContainerAppManagedEnvironment>
{
    public ContainerAppManagedEnvironmentNameResolver(IEnvironmentNameResolver environmentNameResolver)
        : base(environmentNameResolver)
    {

    }

    public override string ResolveName(ContainerAppManagedEnvironment resource, NameResolutionContext context)
    {
        // allow hyphen even though aspire name requirements dictate otherwise
        context.Separator = "-";

        return base.ResolveName(resource, context);
    }
}
