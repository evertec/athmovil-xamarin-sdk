using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ATHMovil.Purchase.Internal;
using Microsoft.Maui.Devices;
using Microsoft.Maui.ApplicationModel;


namespace ATHMovil.Purchase.Utils
{
    public static class NewRelicConfig
    {
        private static readonly HttpClient _httpClient = new HttpClient();


        public static void SendEventToNewRelic(
            string eventType,
            string? paymentStatus = null,
            string? buildType = null,
            string? paymentReference = null,
            string? merchantAppId = null
            )
        {
            Task.Run(() => SendEventAsync(eventType, merchantAppId, paymentStatus, buildType, paymentReference));
        }

        private static async Task SendEventAsync(
            string eventType,
            string? merchantAppId,
            string? paymentStatus,
            string? buildType,
            string? paymentReference
            )
        {
            try
            {
                string insertKey = Regex.Replace(NewRelicConstants.NR.NR_CONSTANT, NewRelicConstants.NR.NRVARIABLES, "");
                string url = Regex.Replace(NewRelicConstants.NR.URL_CONSTANT, NewRelicConstants.NR.NRVARIABLES, "");

                string finalBuildType = string.IsNullOrEmpty(buildType) ? "PROD" : buildType;
                string platform = GetPlatform();

                var eventData = new
                {
                    eventType = eventType,
                    merchant_app_id = merchantAppId ?? string.Empty,
                    timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    payment_reference = paymentReference ?? string.Empty,
                    sdk_platform = platform,
                    build_type = finalBuildType,
                    payment_status = paymentStatus ?? string.Empty,
                    sdk_version = "6.1.1", //AppInfo.Current.VersionString,
                    device_os_version = DeviceInfo.VersionString,
                    device_os_model = DeviceInfo.Model
                };

                var jsonContent = JsonSerializer.Serialize(eventData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("X-Insert-Key", insertKey);
                //request.Headers.Add("Content-Type", "application/json");
                request.Content = content;

                using var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("[NewRelic] Event sent successfully");
                }
                else
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[NewRelic] API error: {(int)response.StatusCode}");
                    Console.WriteLine($"[NewRelic] Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NewRelic] Error: {ex.Message}");
            }
        }

        private static string GetPlatform()
        {
#if ANDROID
            return "Android_Xamarin";
#elif IOS
            return "iOS_Xamarin";
#else
            return "Unknown";
#endif
        }
    }
}