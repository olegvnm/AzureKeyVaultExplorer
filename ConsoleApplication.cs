using AzureKeyVaultExplorer.Services.Interfaces;
using AzureKeyVaultExplorer.Startup.ApplicationRun;
using Microsoft.Extensions.Configuration;
using System;

namespace AzureKeyVaultExplorer
{
    public class ConsoleApplication
    {
        private readonly IConfigurationRoot _config;
        private readonly IApplicationRunner _applicationRunner;
        private readonly IDirectoryService _directoryService;

        public ConsoleApplication(IConfigurationRoot config, IApplicationRunner applicationRunner, IDirectoryService directoryService)
        {
            _config = config;
            _applicationRunner = applicationRunner;
            _directoryService = directoryService;
        }

        public void Run()
        {
            _applicationRunner.Run();

            Console.WriteLine("\nDone! Press any key to close the window.");
            Console.ReadKey(true);

            if (bool.TryParse(_config["OpenDirectoryOnFinish"], out var openDir) && openDir)
                _directoryService.OpenResultsDirectory();
        }
    }
}