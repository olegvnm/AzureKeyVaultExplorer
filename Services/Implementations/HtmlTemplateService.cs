using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using DotLiquid;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    public class HtmlTemplateService : ITemplateService
    {
        private readonly IAssetsProvider _assetsProvider;

        public HtmlTemplateService(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public RenderedTemplate Render(string pageTitle, string tableTitle, IConfigurationRoot secretsConfiguration)
        {
            var template = File.ReadAllText(@$"{Constant.AssetsDirectoryTitle}\{Constant.SecretsTemplateFileTitle}");

            var configurationProvider = secretsConfiguration
                .Providers
                .First();

            var configs = (IDictionary<string, string>)configurationProvider
                .GetType()
                .GetRuntimeProperties()
                .First(x => x.Name == "Data")
                .GetValue(configurationProvider);

            var config = configs.Select(x => new Secret { Key = x.Key, Value = x.Value });

            return new RenderedTemplate
            {
                Title = pageTitle, 
                Template = Render(template, pageTitle, tableTitle, _assetsProvider.GetStyles(), _assetsProvider.GetScripts(), config)
            };
        }

        private string Render(string template, string pageTitle, string tableTitle, IEnumerable<string> styles, IEnumerable<string> scripts, IEnumerable<Secret> secrets)
        {
            secrets = secrets.ToList();

            var parsedTemplate = Template.Parse(template);
            return parsedTemplate.Render(Hash.FromAnonymousObject(new
            {
                pageTitle,
                tableTitle,
                base64Icon = _assetsProvider.GetBase64Favicon(),
                styles,
                scripts,
                secrets, 
                count = secrets.Count()
            }));
        }
    }
}