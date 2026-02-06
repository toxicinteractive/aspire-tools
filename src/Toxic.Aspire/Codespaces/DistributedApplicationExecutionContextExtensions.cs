
namespace Toxic.Aspire.Codespaces;

/// <summary>
/// Extensions for the App Host execution context.
/// </summary>
public static class DistributedApplicationExecutionContextExtensions
{
    extension(DistributedApplicationExecutionContext context)
    {
        /// <summary>
        /// Returns true if the application is running in a GitHub Codespace.
        /// </summary>
        public bool IsCodespace =>
            bool.TryParse(
                Environment.GetEnvironmentVariable(CodespacesOptions.IsCodespaceConfigName),
                out var val)
                && val;
    }
}
