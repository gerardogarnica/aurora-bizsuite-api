using System.Reflection;

namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal static class UtilsSeed
{
    internal const string AdminApplicationCode = "25EE60E9-A6A9-45E8-A899-752C4B4576DC";
    internal const string AdminRoleName = "Administradores";

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