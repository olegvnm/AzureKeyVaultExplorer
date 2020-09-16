using Microsoft.Extensions.Configuration;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IConfigurationHelper
    {
        IConfiguration BuildKeyVaultConfiguration(string vaultEnvironment, string vaultEndpoint, string clientId,
            string clientSecret);
    }
}