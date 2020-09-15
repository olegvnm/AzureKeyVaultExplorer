using Microsoft.Extensions.Configuration;
using System;

namespace AzureKeyVaultExplorer.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationRoot BuildFromJson(this IConfigurationBuilder builder)
        {

            return builder
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
        
        public static bool IsTrue(this IConfigurationRoot config, string key)
        {
            return bool.TryParse(config[key], out var valueIsTrue) && valueIsTrue;
        }
    }
}