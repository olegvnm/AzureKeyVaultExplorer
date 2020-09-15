using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using System;

namespace AzureKeyVaultExplorer.Startup.ApplicationRun
{
    public class MockedSecretsFetcher : IApplicationRunner
    {
        private readonly ITemplateService _templateService;
        private readonly IMockedConfigurationProvider _mockedConfigurationProvider;
        private readonly IFileService _fileService;

        public MockedSecretsFetcher(ITemplateService templateService, IMockedConfigurationProvider mockedConfigurationProvider, IFileService fileService)
        {
            _templateService = templateService;
            _mockedConfigurationProvider = mockedConfigurationProvider;
            _fileService = fileService;
        }

        public void Run()
        {
            Console.WriteLine("Test flow started. Key vault secrets will be mocked.");

            RenderedTemplate renderedTemplates = _templateService.Render("Azure Mocked Key Vault Secrets", "https://mocked-secrets.vault.azure.net/", _mockedConfigurationProvider.Get());
            _fileService.Create(renderedTemplates);
        }
    }
}