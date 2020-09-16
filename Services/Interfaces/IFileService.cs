using AzureKeyVaultExplorer.Models;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IFileService
    {
        void Create(RenderedTemplate template);
    }
}