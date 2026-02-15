using Azure.Core;

namespace AspireTools.NamingConventions;

/// <summary>
/// Contains abbreviations for common Azure regions.
/// </summary>
public static class RegionNames
{
    public const string SwedenCentral = "swc";
    public const string WestEurope = "euw";

    internal static readonly IDictionary<string, string> Regions = new Dictionary<string, string>
    {
        { AzureLocation.SwedenCentral.Name, SwedenCentral },
        { AzureLocation.WestEurope.Name, WestEurope }
    };

    /// <summary>
    /// Gets a common abbreviation for an Azure location name.
    /// </summary>
    public static string GetRegionName(AzureLocation location)
    {
        if (Regions.TryGetValue(location.Name, out var regionName))
        {
            return regionName;
        }

        throw new ArgumentException($"Unrecognized location: {location.Name}");
    }
}
