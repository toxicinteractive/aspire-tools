using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AspireTools.Codespaces;

/// <summary>
/// Initializes a <see cref="CodespacesOptions"/> object.
/// </summary>
internal class CodespacesOptionsConfigurator : IConfigureOptions<CodespacesOptions>
{
    private readonly IConfiguration _configuration;

    public CodespacesOptionsConfigurator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(CodespacesOptions options)
    {
        if (!_configuration.GetValue<bool>(CodespacesOptions.IsCodespaceConfigName, false))
        {
            options.IsCodespace = false;
            return;
        }

        options.IsCodespace = true;
        options.CodespaceName = _configuration[CodespacesOptions.CodespaceNameConfigName];
        options.PortForwardingDomain = _configuration[CodespacesOptions.PortForwardingDomainConfigName];
    }
}
