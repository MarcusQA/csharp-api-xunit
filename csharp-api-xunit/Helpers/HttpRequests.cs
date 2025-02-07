using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace csharp_api_xunit.Helpers
{
    public class HttpRequests
    {
        public static async Task<HttpResponseMessage> MakeHttpRequestAsync(HttpMethod method, string uri, Dictionary<string, string>? headers = null, string? body = null, bool printCurl = true)
        {

            var request = new HttpRequestMessage(method, uri);

            if (headers != null)
            {
                if (headers.TryGetValue("Content-Type", out string contentTypeValue))
                {
                    request.Content = new StringContent(body, Encoding.UTF8, contentTypeValue);
                    headers.Remove("Content-Type");
                }

                foreach (var nonContentHeader in headers)
                {
                    request.Headers.Add(nonContentHeader.Key, nonContentHeader.Value);
                }
            }

            // Only print out cURL when the request does not contain sensitive data such as secrets
            if (printCurl)
            {
                PrintCurlCommand(request);
            }

            var _httpClient = new HttpClient();
            var response = await _httpClient.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("\n========= HTTP response =========");
            Console.WriteLine($"Response status code: {response.StatusCode}");
            Console.WriteLine("\nResponse headers:");
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
            }
            Console.WriteLine("\nResponse body:");
            Console.WriteLine(PrettyFormatJson(responseBody));
            Console.WriteLine("=================================\n");
            return response;
        }

        private static void PrintCurlCommand(HttpRequestMessage request)
        {
            var curl = new StringBuilder($"curl -v -X {request.Method} \"{request.RequestUri}\"");

            foreach (var header in request.Headers)
            {
                curl.Append($" -H \"{header.Key}: {string.Join(", ", header.Value)}\"");
            }

            if (request.Content?.Headers != null)
            {
                foreach (var contentHeader in request.Content.Headers)
                {
                    curl.Append($" -H \"{contentHeader.Key}: {string.Join(", ", contentHeader.Value)}\"");
                }
            }

            if (request.Content != null)
            {
                var content = request.Content.ReadAsStringAsync().Result;
                var contentWithEscapedDoubleQuotes = JsonConvert.SerializeObject(content);
                curl.Append($" -d {contentWithEscapedDoubleQuotes}");
            }

            Console.WriteLine("\n========= cURL request =========");
            Console.WriteLine(curl.ToString());
            Console.WriteLine("================================\n");
        }

        private static string PrettyFormatJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return "{}";
            try
            {
                using var jDoc = JsonDocument.Parse(json);
                return System.Text.Json.JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return json;
            }
        }
    }
}
