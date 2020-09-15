using AzureKeyVaultExplorer.Constants;
using AzureKeyVaultExplorer.Extensions;
using AzureKeyVaultExplorer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yahoo.Yui.Compressor;

namespace AzureKeyVaultExplorer.Services.Implementations
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly IConfigurationRoot _config;
        private readonly ICssCompressor _cssCompressor;
        private readonly IJavaScriptCompressor _javaScriptCompressor;

        public AssetsProvider(IConfigurationRoot config, ICssCompressor cssCompressor, IJavaScriptCompressor javaScriptCompressor)
        {
            _config = config;
            _cssCompressor = cssCompressor;
            _javaScriptCompressor = javaScriptCompressor;
        }

        private readonly IEnumerable<string> _stylesPaths = new[]
        {
            @$"{Constant.AssetsDirectoryTitle}\vendor\bootstrap\bootstrap.min.css",
            @$"{Constant.AssetsDirectoryTitle}\vendor\toastify\toastify.css",
            @$"{Constant.AssetsDirectoryTitle}\css\main.css"
        };

        private readonly IEnumerable<string> _scriptsPaths = new[]
        {
            @$"{Constant.AssetsDirectoryTitle}\vendor\toastify\toastify-transpiled.js",
            @$"{Constant.AssetsDirectoryTitle}\js\main-transpiled.js"
        };

        private readonly string _keyVaultSvgIconPath = @$"{Constant.AssetsDirectoryTitle}\images\icons\key-vault.svg";

        public IEnumerable<string> GetStyles()
        {
            return _stylesPaths
                .Select(File.ReadAllText)
                .Select(x => _config.IsTrue("MinifyCss") ? _cssCompressor.Compress(x) : x);
        }

        public IEnumerable<string> GetScripts()
        {
            return _scriptsPaths
                .Select(File.ReadAllText)
                //Warning: compression works only on transpiled scrips
                .Select(x => _config.IsTrue("ObfuscateJs") ? _javaScriptCompressor.Compress(x) : x); 
        }

        public string GetBase64Favicon()
        {
            string iconText = File.ReadAllText(_keyVaultSvgIconPath);
            return Encoding.UTF8.EncodeBase64(iconText);
        }
    }
}