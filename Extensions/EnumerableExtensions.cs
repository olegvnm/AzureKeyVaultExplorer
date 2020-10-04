using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Services.Interfaces.Template;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AzureKeyVaultExplorer.Extensions
{
    public static class EnumerableExtensions
    {
        public static IDictionary<string, IConfigurationRoot> ToConfigurationDictionary(
            this IEnumerable<KeyVault> keyVaults,
            IConfigurationHelper configurationHelper,
            IConfigurationRoot config)
        {
            return keyVaults.ToDictionary(x => x.Title,
                x => (IConfigurationRoot) configurationHelper.BuildKeyVaultConfiguration(x.Title, x.Endpoint,
                    config[$"{x.Title}AppClientId"], config[$"{x.Title}AppClientSecret"]));
        }

        public static IEnumerable<RenderedTemplate> ToRenderedTemplates(
            this IDictionary<string, IConfigurationRoot> configurationDictionary,
            ITemplateService templateService,
            IConfigurationRoot config)
        {
            return configurationDictionary.Select(x => templateService.Render($"Azure {x.Key} Key Vault Secrets",
                $"{x.Key}: " + config[$"KeyVault{x.Key}"], x.Value.GetFirstProvider().GetConfigurationDictionary()));
        }
    }
}