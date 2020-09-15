using System;
using System.Linq;

namespace AzureKeyVaultExplorer.Exceptions
{
    public class ConfigException : Exception
    {
        public ConfigException(string message) : base($"\n{message}")
        {

        }

        public ConfigException(params string[] configs) : base(
            $"\nError: configs incorrect setup. {string.Join(", ", configs.Select(x => $"'{x}'"))} not set or set incorrectly.")
        {

        }
    }
}