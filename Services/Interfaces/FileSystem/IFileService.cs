using AzureKeyVaultExplorer.Models;

namespace AzureKeyVaultExplorer.Services.Interfaces.FileSystem
{
    public interface IFileService
    {
        void Create(RenderedTemplate template);
    }
}