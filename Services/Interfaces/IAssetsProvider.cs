using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IAssetsProvider
    {
        public IEnumerable<string> GetStyles();
        public IEnumerable<string> GetScripts();
        public string GetBase64Favicon();
    }
}