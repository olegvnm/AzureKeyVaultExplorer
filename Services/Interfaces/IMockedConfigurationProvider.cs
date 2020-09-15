using Microsoft.Extensions.Configuration;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IMockedConfigurationProvider
    {
        public IConfigurationRoot Get();
    }
}