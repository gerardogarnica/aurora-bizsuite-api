namespace Aurora.BizSuite.Api.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder builder, string[] moduleNames)
    {
        foreach (var module in moduleNames)
        {
            builder.AddJsonFile($"modules.{module}.json", false, true);
            builder.AddJsonFile($"modules.{module}.Development.json", true, true);
        }
    }
}