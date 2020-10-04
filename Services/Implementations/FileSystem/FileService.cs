using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Models;
using AzureKeyVaultExplorer.Services.Interfaces.FileSystem;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AzureKeyVaultExplorer.Services.Implementations.FileSystem
{
    public class FileService : IFileService
    {
        public void Create(RenderedTemplate template)
        {
            var fileTitleWithExtension = $"{Regex.Replace(template.Title, @"\s+", "")}.{template.FileExtension}";
            File.AppendAllText(@$"{Constant.ResultsDirectoryTitle}\{fileTitleWithExtension}", template.Template);
            Console.WriteLine($"{fileTitleWithExtension} file created!");
        }
    }
}