using AzureKeyVaultExplorer.Exceptions;
using AzureKeyVaultExplorer.Extensions;
using AzureKeyVaultExplorer.Services.Implementations;
using AzureKeyVaultExplorer.Services.Implementations.FileSystem;
using AzureKeyVaultExplorer.Services.Implementations.Template;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Services.Interfaces.FileSystem;
using AzureKeyVaultExplorer.Services.Interfaces.Template;
using AzureKeyVaultExplorer.Startup.ApplicationRun;
using AzureKeyVaultExplorer.Validators.Implementations;
using AzureKeyVaultExplorer.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Yahoo.Yui.Compressor;

namespace AzureKeyVaultExplorer.Startup
{
    public static class Services
    {
        public static IServiceProvider Register()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IAssetsProvider, AssetsProvider>();
            services.AddSingleton<IKeyVaultValidator, KeyVaultValidator>();
            services.AddSingleton<IClientCredsValidator, ClientCredsValidator>();
            services.AddSingleton<IConfigurationHelper, ConfigurationHelper>();
            services.AddSingleton<IKeyVaultsProvider, KeyVaultsProvider>();
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IMockedConfigurationProvider, MockedConfigurationProvider>();
            services.AddSingleton<ICssCompressor, CssCompressor>();
            services.AddSingleton<IJavaScriptCompressor, JavaScriptCompressor>();
            services.AddSingleton<ConsoleApplication>();

            IConfigurationRoot config = new ConfigurationBuilder().BuildFromJson();
            services.AddSingleton<IConfigurationRoot>(provider => config);

            if (config.IsTrue("IsTestRun"))
                services.AddSingleton<IApplicationRunner, MockedSecretsFetcher>();
            else
                services.AddSingleton<IApplicationRunner, KeyVaultSecretsFetcher>();

            var renderFormatConfigName = "RenderFormat";
            switch (config[renderFormatConfigName]?.ToLowerInvariant())
            {
                case "html":
                    services.AddSingleton<ITemplateService, HtmlTemplateService>();
                    break;
                case "json":
                    services.AddSingleton<ITemplateService, JsonTemplateService>();
                    break;
                default:
                    services.AddSingleton<ITemplateService, HtmlTemplateService>();
                    break;
            }

            return services.BuildServiceProvider(true);
        }
    }
}