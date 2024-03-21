using System.Reflection;

namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal static class UtilsSeed
{
    internal static string GetSeedDataPath(string fileName)
    {
        string? location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (string.IsNullOrWhiteSpace(location))
        {
            throw new InvalidOperationException("The location of the executing assembly is not found.");
        }

        return Path.Combine(location, "Seeds", "Data", fileName);
    }
}