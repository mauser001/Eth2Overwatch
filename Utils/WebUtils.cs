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
        private static readonly HttpClient _httpClient = new();

        public static bool URLExists(string uri)
        {
            bool result = true;

            try
            {
                var task = Task.Run(() => _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri)));
                task.Wait();
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public static string FetchInfo(string uri)
        {
            var task = Task.Run(() => _httpClient.GetStringAsync(uri));
            task.Wait();
            return task.Result;
        }

        public static void SendData(string uri, string data)
        {
            var task = Task.Run(async () =>
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsStringAsync();
                }
            });
            task.Wait();
        }

        public static async void DownloadFileAsync(string uri
             , string outputPath, string fileName, Action success)
        {

            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult))
                throw new InvalidOperationException("URI is invalid.");

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMinutes(60));
            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(outputPath+fileName, fileBytes, cts.Token);

            success();
        }
    }
}
