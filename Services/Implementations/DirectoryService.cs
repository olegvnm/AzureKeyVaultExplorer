using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Services.Interfaces;
using System.Diagnostics;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    class DirectoryService : IDirectoryService
    {
        public void OpenResultsDirectory()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = Constant.ResultsDirectoryTitle,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
    }
}