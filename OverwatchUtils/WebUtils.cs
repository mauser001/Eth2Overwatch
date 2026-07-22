using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eth2Overwatch.OverwatchUtils
{
    public static class WebUtils
    {
        private static readonly HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        public static bool URLExists(string uri)
        {
            bool result = true;

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, uri);
                using var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
                result = response.IsSuccessStatusCode;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public static string FetchInfo(string uri)
        {
            return _httpClient.GetStringAsync(uri).GetAwaiter().GetResult();
        }

        public static void SendData(string uri, string data)
        {
            using var content = new StringContent(data, Encoding.UTF8, "application/json");
            using var response = _httpClient.PostAsync(uri, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }

        public static string PostJson(string uri, string data)
        {
            using var content = new StringContent(data, Encoding.UTF8, "application/json");
            using var response = _httpClient.PostAsync(uri, content).GetAwaiter().GetResult();
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static async void DownloadFileAsync(string uri
             , string outputPath, string fileName, Action success)
        {
            try
            {
                if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult))
                    throw new InvalidOperationException("URI is invalid.");

                using var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromMinutes(60));
                using var downloadClient = new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(60)
                };
                byte[] fileBytes = await downloadClient.GetByteArrayAsync(uriResult, cts.Token);
                await File.WriteAllBytesAsync(outputPath + fileName, fileBytes, cts.Token);

                success();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error occurred: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

        }
    }
}
