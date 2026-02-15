using Microsoft.Extensions.Options;

namespace AspireTools.Codespaces;

/// <summary>
/// Used to build a url for a resource running in a GitHub Codespace.
/// </summary>
public class CodespaceUrlService
{
    private readonly IOptions<CodespacesOptions> _options;

    public CodespaceUrlService(IOptions<CodespacesOptions> options)
    {
        _options = options;
    }

    /// <summary>
    /// Gets the url to the resource in the currently running GitHub Codespace, or null if the application isn't running in one.
    /// </summary>
    public string? GetCodespaceUrl(IResourceWithEndpoints resource, string endpointName = "https")
    {
        if (!_options.Value.IsCodespace)
        {
            return null;
        }

        var endpoint = resource.GetEndpoint(endpointName);
        var url = new Uri(endpoint.Url);

        return $"{url.Scheme}://{_options.Value.CodespaceName}-{url.Port}.{_options.Value.PortForwardingDomain}{url.AbsolutePath}{url.Query}";
    }
}
