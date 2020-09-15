using AzureKeyVaultExplorer.Exceptions;
using AzureKeyVaultExplorer.Validators.Interfaces;

namespace AzureKeyVaultExplorer.Validators.Implementations
{
    public class ClientCredsValidator : IClientCredsValidator
    {
        public void Validate(string vaultEnvironment, string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                throw new ConfigException($"{vaultEnvironment}AppClientId", $"{vaultEnvironment}AppClientSecret");
        }
    }
}