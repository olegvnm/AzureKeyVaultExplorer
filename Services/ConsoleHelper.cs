using AzureKeyVaultExplorer.Models;
using System;
using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Services
{
    public class ConsoleHelper
    {
        public static void PrintKeyVaultSelectingMessage(Dictionary<int, KeyVault> allKeyVaults)
        {
            Console.WriteLine("Select key vault (press 'Enter' to process all key vaults):\n");
            foreach (var keyVault in allKeyVaults)
            {
                Console.WriteLine($"{keyVault.Key}:\t{keyVault.Value.Title}\t   {keyVault.Value.Endpoint}");
            }
        }
    }
}