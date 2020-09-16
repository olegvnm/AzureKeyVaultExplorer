namespace AzureKeyVaultExplorer.Validators.Interfaces
{
    public interface IClientCredsValidator
    {
        void Validate(string vaultEnvironment, string clientId, string clientSecret);
    }
}