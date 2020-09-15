using DotLiquid;

namespace AzureKeyVaultExplorer.Models
{
    public class Secret : Drop
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}