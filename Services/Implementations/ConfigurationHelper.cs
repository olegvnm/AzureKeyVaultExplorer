using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Net.Http;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        private readonly IKeyVaultValidator _keyVaultValidator;
        private readonly IClientCredsValidator _clientCredsValidator;

        public ConfigurationHelper(IKeyVaultValidator keyVaultValidator, IClientCredsValidator clientCredsValidator)
        {
            _keyVaultValidator = keyVaultValidator;
            _clientCredsValidator = clientCredsValidator;
        }

        public IConfiguration BuildKeyVaultConfiguration(string vaultEnvironment, string vaultEndpoint, string clientId, string clientSecret)
        {
            _keyVaultValidator.ValidateEndpoint(vaultEndpoint, new ArgumentException("Argument is null or whitespace", nameof(vaultEndpoint)));
            _clientCredsValidator.Validate(vaultEnvironment, clientId, clientSecret);

            IConfigurationRoot config = null;
            try
            {
                config = new ConfigurationBuilder()
                    .AddAzureKeyVault(vaultEndpoint, clientId, clientSecret)
                    .Build();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequest Exception occured while fetching '{vaultEnvironment}': '{vaultEndpoint}' key vault.\nException message: {ex.Message}\nPress any key to exit...");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            catch (AdalServiceException ex)
            {
                Console.WriteLine($"Exception occured while fetching '{vaultEnvironment}': '{vaultEndpoint}' key vault.\nCredentials incorrect.\nException message: {ex.Message}\nPress any key to exit...");
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            Console.WriteLine($"{vaultEnvironment} successfully fetched.");
            return config;
        }
    }
}