using Azure.Provisioning.Sql;

namespace Toxic.Aspire.NamingConventions.NameResolvers.Resources;

public class SqlDatabaseNameResolver : DefaultResourceNameResolver<SqlDatabase>
{
    public SqlDatabaseNameResolver(IEnvironmentNameResolver environmentNameResolver)
        : base(environmentNameResolver)
    {

    }

    public override string ResolveName(SqlDatabase resource, NameResolutionContext context)
    {
        // don't include region name in database name
        context.SupportsRegion = false;

        return base.ResolveName(resource, context);
    }
}
