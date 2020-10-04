using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces.Template;
using DotLiquid;
using JsonFormatterPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AzureKeyVaultExplorer.Services.Implementations.Template
{
    public class JsonTemplateService : ITemplateService
    {
        public RenderedTemplate Render(string pageTitle, string tableTitle, IDictionary<string, string> configs)
        {
            var template = File.ReadAllText(@$"{Constant.AssetsDirectoryTitle}\{Constant.JsonTemplateFileTitle}");

            var secrets = configs.Select(x => new Secret { Key = x.Key, Value = x.Value }).ToList();

            var parsedJsonTemplate = DotLiquid.Template.Parse(template);
            var renderedJson = parsedJsonTemplate.Render(Hash.FromAnonymousObject(new { secrets }));

            return new RenderedTemplate
            {
                Title = pageTitle,
                Template = JsonFormatter.Format(renderedJson),
                FileExtension = "json"
            };
        }
    }
}