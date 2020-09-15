namespace AzureKeyVaultExplorer.Validators.Interfaces
{
    public interface IClientCredsValidator
    {
        public void Validate(string vaultEnvironment, string clientId, string clientSecret);
    }
}