namespace Toxic.Aspire.Codespaces;

/// <summary>
/// Provides info about the active GitHub Codespace when the application is running in one.
/// </summary>
public class CodespacesOptions
{
    public const string IsCodespaceConfigName = "CODESPACES";
    public const string CodespaceNameConfigName = "CODESPACE_NAME";
    public const string PortForwardingDomainConfigName = "GITHUB_CODESPACES_PORT_FORWARDING_DOMAIN";

    /// <summary>
    /// Gets whether the application is currently running inside a GitHub Codespace.
    /// </summary>
    public bool IsCodespace { get; set; }

    /// <summary>
    /// Returns the name of the currently running Codespace, or null if the application isn't running in a Codespace.
    /// </summary>
    public string? CodespaceName { get; set; }

    /// <summary>
    /// Returns the domain of the currently running Codespace, or null if the application isn't running in a Codespace.
    /// </summary>
    public string? PortForwardingDomain { get; set; }
}
