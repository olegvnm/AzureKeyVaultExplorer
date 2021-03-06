﻿using AzureKeyVaultExplorer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AzureKeyVaultExplorer.Services.Interfaces
{
    public interface IKeyVaultsProvider
    {
        Dictionary<int, KeyVault> GetFromConfig(IConfigurationRoot config);
    }
}