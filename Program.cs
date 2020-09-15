using AzureKeyVaultExplorer.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureKeyVaultExplorer
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main()
        {
            _serviceProvider = Startup.Services.Register();

            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();

            _serviceProvider.DisposeServices();
        }
    }
}
