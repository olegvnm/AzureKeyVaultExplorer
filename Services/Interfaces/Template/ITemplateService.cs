using AzureKeyVaultExplorer.Models;
using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Services.Interfaces.Template
{
    public interface ITemplateService
    {
        RenderedTemplate Render(string pageTitle, string tableTitle, IDictionary<string, string> configss);
    }
}