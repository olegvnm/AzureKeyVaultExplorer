using AzureKeyVaultExplorer.Models;
using System;
using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Validators.Interfaces
{
    public interface IKeyVaultValidator
    {
        public void ValidateNotEmpty(Dictionary<int, KeyVault> keyVaultsToProcess, Exception ex);
        public void ValidateNotEmpty(IEnumerable<KeyValuePair<string, string>> allKeyVaults, Exception ex);
        public void ValidateEndpoint(string endpoint, Exception ex);
    }
}