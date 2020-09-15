using AzureKeyVaultExplorer.Models;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IFileService
    {
        public void Create(RenderedTemplate template);
    }
}