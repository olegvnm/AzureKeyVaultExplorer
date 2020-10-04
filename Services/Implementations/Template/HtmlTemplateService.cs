using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Services.Interfaces.Template;
using DotLiquid;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AzureKeyVaultExplorer.Services.Implementations.Template
{
    public class HtmlTemplateService : ITemplateService
    {
        private readonly IAssetsProvider _assetsProvider;

        public HtmlTemplateService(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public RenderedTemplate Render(string pageTitle, string tableTitle, IDictionary<string, string> configs)
        {
            var template = File.ReadAllText(@$"{Constant.AssetsDirectoryTitle}\{Constant.HtmlTemplateFileTitle}");
            var secrets = configs.Select(x => new Secret { Key = x.Key, Value = x.Value }).ToList();

            var parsedHtmlTemplate = DotLiquid.Template.Parse(template);
            var renderedTemplate = parsedHtmlTemplate.Render(Hash.FromAnonymousObject(new
            {
                pageTitle,
                tableTitle,
                base64Icon = _assetsProvider.GetBase64Favicon(),
                styles = _assetsProvider.GetStyles(),
                scripts = _assetsProvider.GetScripts(),
                secrets,
                count = secrets.Count()
            }));

            return new RenderedTemplate
            {
                Title = pageTitle, 
                Template = renderedTemplate,
                FileExtension = "html"
            };
        }
    }
}