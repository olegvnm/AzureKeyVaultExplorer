using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Services.Interfaces.FileSystem;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AzureKeyVaultExplorer.Services.Implementations.FileSystem
{
    class DirectoryService : IDirectoryService
    {
        public void PrepareResultsDirectory()
        {
            var directoryInfo = Directory.CreateDirectory(Constant.ResultsDirectoryTitle);
            directoryInfo.GetFiles().ToList().ForEach(f => f.Delete());
        }

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