using System;
using System.IO;
using System.Reflection;

namespace AzureKeyVaultExplorer.Constants
{
    public static class Constant
    {
        public static string KeyVault = "KeyVault";
        public static string SecretsTemplateFileTitle = "secretsTemplate.html";
        public static string AssetsDirectoryTitle = @$"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\Assets";
        public static string ResultsDirectoryTitle = @$"{Environment.CurrentDirectory}\Results";
    }
}