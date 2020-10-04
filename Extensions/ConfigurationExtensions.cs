using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static IConfigurationProvider GetFirstProvider(this IConfigurationRoot config)
        {
            return config.Providers.First();
        }

        public static IDictionary<string, string> GetConfigurationDictionary(this IConfigurationProvider configProvider)
        {
            return (IDictionary<string, string>)configProvider
                .GetType()
                .GetRuntimeProperties()
                .First(x => x.Name == "Data")
                .GetValue(configProvider);
        }
    }
}