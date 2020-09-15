namespace AzureKeyVaultExplorer.Extensions
{
    public static class EncodingExtensions
    {
        public static string EncodeBase64(this System.Text.Encoding encoding, string text)
        {
            if (text == null)
                return null;

            byte[] textAsBytes = encoding.GetBytes(text);
            return System.Convert.ToBase64String(textAsBytes);
        }
    }
}