using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    public class HtmlFileService : IFileService
    {
        public void Create(RenderedTemplate template)
        {
            var directoryInfo = Directory.CreateDirectory(Constant.ResultsDirectoryTitle);
            directoryInfo.GetFiles().ToList().ForEach(f => f.Delete());

            Console.WriteLine();

            var fileTitle = Regex.Replace(template.Title, @"\s+", "");
            File.AppendAllText(@$"{Constant.ResultsDirectoryTitle}\{fileTitle}.html", template.Template);
            Console.WriteLine($"{fileTitle}.html file created!");
        }
    }
}