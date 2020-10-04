using AzureKeyVaultExplorer.Extensions;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Services.Interfaces.FileSystem;
using AzureKeyVaultExplorer.Services.Interfaces.Template;
using AzureKeyVaultExplorer.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AzureKeyVaultExplorer.Startup.ApplicationRun
{
    public class KeyVaultSecretsFetcher : IApplicationRunner
    {
        private readonly IConfigurationRoot _config;
        private readonly IDirectoryService _directoryService;

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
            IConfigurationRoot config,
            IDirectoryService directoryService)
        {
            _fileService = fileService;
            _templateService = templateService;
            _keyVaultsProvider = keyVaultsProvider;
            _keyVaultValidator = keyVaultValidator;
            _configurationHelper = configurationHelper;
            _config = config;
            _directoryService = directoryService;
        }

        public void Run()
        {
            var configKeyVaults = _keyVaultsProvider.GetFromConfig(_config);

            ConsoleHelper.PrintKeyVaultSelectingMessage(configKeyVaults);
            var readKey = Console.ReadKey(true);

            var keyVaultsToProcess = configKeyVaults
                .Where(x => readKey.Key.Equals(ConsoleKey.Enter) || x.Key == int.Parse(readKey.KeyChar.ToString()))
                .ToDictionary(x => x.Key, x => x.Value);

            _keyVaultValidator.ValidateNotEmpty(keyVaultsToProcess, new ArgumentException("\nIndex do not exist!"));

            Console.WriteLine($"\nSelected key vaults: \n\n{string.Join("\n", keyVaultsToProcess.Select(x => x.Value.Title))}\n");

            var renderedTemplates = keyVaultsToProcess.Select(x => new KeyVault { Title = x.Value.Title, Endpoint = x.Value.Endpoint })
                .ToConfigurationDictionary(_configurationHelper, _config)
                .ToRenderedTemplates(_templateService, _config);

            Console.WriteLine();

            _directoryService.PrepareResultsDirectory();
            renderedTemplates.ToList().ForEach(x => _fileService.Create(x));
        }
    }
}