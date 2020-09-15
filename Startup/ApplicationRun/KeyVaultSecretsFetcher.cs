using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AzureKeyVaultExplorer.Startup.ApplicationRun
{
    public class KeyVaultSecretsFetcher : IApplicationRunner
    {
        private readonly IConfigurationRoot _config;

        private readonly IFileService _fileService;
        private readonly ITemplateService _templateService;
        private readonly IKeyVaultsProvider _keyVaultsProvider;
        private readonly IKeyVaultValidator _keyVaultValidator;
        private readonly IConfigurationHelper _configurationHelper;

        public KeyVaultSecretsFetcher(IFileService fileService,
            ITemplateService templateService,
            IKeyVaultsProvider keyVaultsProvider,
            IKeyVaultValidator keyVaultValidator,
            IConfigurationHelper configurationHelper,
            IConfigurationRoot config)
        {
            _fileService = fileService;
            _templateService = templateService;
            _keyVaultsProvider = keyVaultsProvider;
            _keyVaultValidator = keyVaultValidator;
            _configurationHelper = configurationHelper;
            _config = config;

        }

        public void Run()
        {
            var allKeyVaults = _keyVaultsProvider.GetFromConfig(_config);

            ConsoleHelper.PrintKeyVaultSelectingMessage(allKeyVaults);
            var readKey = Console.ReadKey(true);

            var keyVaultsToProcess = allKeyVaults
                .Where(x => readKey.Key.Equals(ConsoleKey.Enter) || x.Key == int.Parse(readKey.KeyChar.ToString()))
                .ToDictionary(x => x.Key, x => x.Value);

            _keyVaultValidator.ValidateNotEmpty(keyVaultsToProcess, new ArgumentException("\nIndex do not exist!"));

            Console.WriteLine($"\nSelected key vaults: \n\n{string.Join("\n", keyVaultsToProcess.Select(x => x.Value.Title))}\n");

            keyVaultsToProcess
                .Select(x => new KeyVault {Title = x.Value.Title, Endpoint = x.Value.Endpoint})
                .ToDictionary(x => x.Title, x => (IConfigurationRoot) _configurationHelper.BuildKeyVaultConfiguration(x.Title, x.Endpoint, _config[$"{x.Title}AppClientId"], _config[$"{x.Title}AppClientSecret"]))
                .Select(x => _templateService.Render($"Azure {x.Key} Key Vault Secrets", $"{x.Key}: " + _config[$"KeyVault{x.Key}"], x.Value))
                .ToList()
                .ForEach(x => _fileService.Create(x));
        }
    }
} 