using AzureKeyVaultExplorer.Models;
using System;
using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Validators.Interfaces
{
    public interface IKeyVaultValidator
    {
        void ValidateNotEmpty(Dictionary<int, KeyVault> keyVaultsToProcess, Exception ex);
        void ValidateNotEmpty(IEnumerable<KeyValuePair<string, string>> allKeyVaults, Exception ex);
        void ValidateEndpoint(string endpoint, Exception ex);
    }
}