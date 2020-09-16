using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IAssetsProvider
    {
        IEnumerable<string> GetStyles();
        IEnumerable<string> GetScripts();
        string GetBase64Favicon();
    }
}