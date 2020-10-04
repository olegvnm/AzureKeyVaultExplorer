using AzureKeyVaultExplorer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    class MockedConfigurationProvider : IMockedConfigurationProvider
    {
        public IConfigurationRoot Get()
        {
            return new ConfigurationRoot(new List<IConfigurationProvider>
            {
                new MemoryConfigurationProvider(new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>
                    {
                        { "Access-Control-Allow-Origin", "https://google.com" },
                        {
                            "Some1ServiceBusConnectionString",
                            "Endpoint=sb://bla-bla-bla-dev.servicebus.windows.net/;SharedAccessKeyName=ListenOnlySharedAccessKey;SharedAccessKey=jfje7HGhd7rwqRl82pqOueMzxuePhdfj="
                        },
                        { "azure-subscription-key", "lsYyu27bzmJheyGsTfSD8lIDhsi3ik&JG8Gj7gfF" },
                        { "someTopic", "someTitle" },
                        {
                            "Some2ServiceBusConnectionString",
                            "Endpoint=sb://bla-bla-bla-dev.servicebus.windows.net/;SharedAccessKeyName=ListenOnlySharedAccessKey;SharedAccessKey=hslKhsieEA38Hl8Y836hasYU/hsiYYTGhsu2a="
                        }
                    }
                })
            });
        }
    }
}