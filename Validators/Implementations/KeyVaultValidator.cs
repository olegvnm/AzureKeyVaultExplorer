using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureKeyVaultExplorer.Validators.Implementations
{
    public class KeyVaultValidator : IKeyVaultValidator
    {
        public void ValidateNotEmpty(Dictionary<int, KeyVault> allKeyVaults, Exception ex)
        {
            if (!allKeyVaults.Any())
                throw ex;
        }

        public void ValidateNotEmpty(IEnumerable<KeyValuePair<string, string>> allKeyVaults, Exception ex)
        {
            if (!allKeyVaults.Any())
                throw ex;
        }

        public void ValidateEndpoint(string endpoint, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw ex;
        }
    }
}