using AzureKeyVaultExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureKeyVaultExplorer.Services
{
    public class ConsoleHelper
    {
        public static void PrintKeyVaultSelectingMessage(Dictionary<int, KeyVault> allKeyVaults)
        {
            Console.WriteLine("Select key vault (press 'Enter' to process all key vaults):\n");
            allKeyVaults.ToList().ForEach(x => Console.WriteLine($"{x.Key}:\t{x.Value.Title}\t   {x.Value.Endpoint}"));
        }
    }
}