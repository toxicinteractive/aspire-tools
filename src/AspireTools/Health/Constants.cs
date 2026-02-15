namespace AspireTools.Health;

internal static class Constants
{
    internal const string HealthEndpointName = "health";
    internal const string ReadynessEndpointPath = "/health/ready";
    internal const string AlivenessEndpointPath = "/health/alive";
    internal const string AlivenessTag = "live";
    internal const int HealthCheckPort = 8081;
}
