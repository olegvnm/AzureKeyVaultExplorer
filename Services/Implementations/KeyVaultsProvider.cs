using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Exceptions;
using AzureKeyVaultExplorer.Extensions;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    public class KeyVaultsProvider : IKeyVaultsProvider
    {
        private readonly IKeyVaultValidator _keyVaultValidator;

        public KeyVaultsProvider(IKeyVaultValidator keyVaultValidator)
        {
            _keyVaultValidator = keyVaultValidator;
        }

        public Dictionary<int, KeyVault> GetFromConfig(IConfigurationRoot config)
        {
            var keyVaults = config
                .AsEnumerable()
                .Where(x => x.Key.StartsWith(Constant.KeyVault))
                .Select(x => new KeyValuePair<string, string>(x.Key.RemoveKeyVaultPrefix(), x.Value))
                .ToList();

            _keyVaultValidator.ValidateNotEmpty(keyVaults, new ConfigException(new[] {"KeyVault[env]"}));
            
            var keyVaultsWithOrdering = keyVaults
                .Select(x => new
                {
                    IsParsed = int.TryParse(config[$"{x.Key}SortOrder"], out int order), 
                    Index = order,
                    KeyVault = x
                })
                .Where(x => x.IsParsed)
                .ToList();

            if (!keyVaultsWithOrdering.Any())
                return keyVaults.Select((x, i) => new { Index = i + 1, KeyVault = x })
                    .ToDictionary(x => x.Index, x => new KeyVault { Title = x.KeyVault.Key, Endpoint = x.KeyVault.Value });

            if (keyVaultsWithOrdering.Count != keyVaults.Count)
                throw new ConfigException("KeyVaults ordering ('[env]Order') incorrect setup!\nOrdering should be set either for all keyvaults or for none!");

            if (keyVaultsWithOrdering.Select(x => x.Index).Distinct().Count() != keyVaults.Count)
                throw new ConfigException("KeyVaults ordering ('[env]Order') incorrect setup!\nOrdering indexes should be distinct!");

            return keyVaultsWithOrdering.OrderBy(x => x.Index)
                .ToDictionary(x => x.Index, x => new KeyVault { Title = x.KeyVault.Key, Endpoint = x.KeyVault.Value });
        }
    }
}