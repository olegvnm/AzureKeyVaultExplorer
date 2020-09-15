using System;

namespace AzureKeyVaultExplorer.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void DisposeServices(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                return;

            if (serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }
    }
}