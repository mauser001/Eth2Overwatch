using System;
using System.IO;
using System.Net.Http;

namespace Eth2Overwatch.OverwatchUtils
{
    public static class WebUtils
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async void DownloadFileAsync(string uri
             , string outputPath, string fileName, Action success)
        {

            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult))
                throw new InvalidOperationException("URI is invalid.");

            /*if (!File.Exists(outputPath))
                throw new FileNotFoundException("File not found."
                   , nameof(outputPath));*/

            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(outputPath+fileName, fileBytes);

            success();
        }
    }
}
