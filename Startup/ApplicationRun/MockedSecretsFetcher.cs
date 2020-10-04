using AzureKeyVaultExplorer.Extensions;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Services.Interfaces.FileSystem;
using AzureKeyVaultExplorer.Services.Interfaces.Template;
using System;

namespace AzureKeyVaultExplorer.Startup.ApplicationRun
{
    public class MockedSecretsFetcher : IApplicationRunner
    {
        private readonly ITemplateService _templateService;
        private readonly IMockedConfigurationProvider _mockedConfigurationProvider;
        private readonly IFileService _fileService;
        private readonly IDirectoryService _directoryService;

        public MockedSecretsFetcher(ITemplateService templateService,
            IMockedConfigurationProvider mockedConfigurationProvider,
            IFileService fileService,
            IDirectoryService directoryService)
        {
            _templateService = templateService;
            _mockedConfigurationProvider = mockedConfigurationProvider;
            _fileService = fileService;
            _directoryService = directoryService;
        }

        public void Run()
        {
            Console.WriteLine("Test flow started. Key vault secrets will be mocked.\n");

            _directoryService.PrepareResultsDirectory();

            RenderedTemplate renderedTemplates = _templateService.Render("Azure Mocked Key Vault Secrets",
                "https://mocked-secrets.vault.azure.net/", _mockedConfigurationProvider.Get().GetFirstProvider().GetConfigurationDictionary());
            _fileService.Create(renderedTemplates);
        }
    }
}