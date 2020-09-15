using AzureKeyVaultExplorer.Constants;

namespace AzureKeyVaultExplorer.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveKeyVaultPrefix(this string value)
        {
            return value.Substring(Constant.KeyVault.Length);
        }
    }
}