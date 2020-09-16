using AzureKeyVaultExplorer.Models;
using Microsoft.Extensions.Configuration;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface ITemplateService
    {
        RenderedTemplate Render(string pageTitle, string tableTitle, IConfigurationRoot secretsConfiguration);
    }
}