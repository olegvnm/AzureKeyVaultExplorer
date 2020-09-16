
# Azure Key Vault Explorer

This is dotnet core application that allows to explore secrets from Azure Key Vaults using key vault's `Url`, `ClientId` and `ClientSecret`. At the end it renders HTML page(s) with all secrets found in key vault(s).

## Getting Started

This section you can read about features, configuring and runing the app. If you want to publish app to single `.exe` file - see section below.

### Running

Run as usual dotnet core application (using `Visual Studio` or console command `dotnet run`)


### Configuring

App works with `appconfig.json` configuration file. Format of file:

```json
{
  "KeyVaultDev": "https://bla-bla-bla-dev.vault.azure.net/",
  "DevAppClientId": "bla-bla-bla-id",
  "DevAppClientSecret": "bla-bla-bla-secret=",
  "DevSortOrder": 1,
 
  "KeyVaultQa": "https://bla-bla-bla-qa.vault.azure.net/",
  "QaAppClientId": "bla-bla-bla-id",
  "QaAppClientSecret": "bla-bla-bla-secret=",
  "QaSortOrder": 2,
 
  "IsTestRun": true,
  "MinifyCss": true,
  "ObfuscateJs": true,
  "OpenDirectoryOnFinish": true
}
```
For each key vault you want to fetch secrets config should contain at least 3 params: `KeyVault`, `AppClientId`, `AppClientSecret`. 

For example if you want to fetch **Prod** key vault, you should provide 'KeyVault**Prod**', '**Prod**AppClientId', '**Prod**AppClientSecret'.

Ordering: you should either specify `SortOrder` for all key vaults or for none. Values must be distinct!


### Features

* If `IsTestRun` is set to true - app will run the test flow (no secrets will be fetched from the real key vaults)
* `MinifyCss` and `ObfuscateJs` let you configure the compressing of the assets. Be aware, obfuscation works only for transpiled scripts
* `OpenDirectoryOnFinish` will open the folder with rendered HTMLs after execution

### HTML UI
Rendered HTML example:

![HTML UI](./Assets/images/screenshot/result2.png?raw=true)

### Fetched results
Fetched secrets are located in `Results` folder. This folder is gets cleaned on every run.

## Publishing

Application can be published to single `.exe` file with `appconfig.json` file alongside. Two ways to do that: 
1. Using Visual Studio (Project Right Click &rarr; Publish &rarr; Select 'SingleExecutable' profile &rarr; Publish)
2. Run `PublishSingleExecutable.ps1` PowerShell script (located in `_publish` folder)

Both options will give you the same output.

## Acknowledgments

This solution uses next tools:
* DotLiquid - NuGet package; HTML templating and rendering
* Toastify - JS and CSS libs;  UI notifications (toasts)
* YUICompressor - NuGet package; CSS minification, JS obfuscation
* Bootstrap - CSS lib; UI styles
