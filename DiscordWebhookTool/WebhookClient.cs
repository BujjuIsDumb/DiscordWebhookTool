using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordWebhookTool
{
    public class WebhookClient : IDisposable
    {
        private HttpClient _client;

        public WebhookClient(string webhookUrl)
        {
            _client = new HttpClient();
            WebhookUrl = webhookUrl;
        }

        public string WebhookUrl
        {
            get => _client.BaseAddress.ToString();
            set => _client.BaseAddress = new Uri(value);
        }

        public async Task<HttpResponseMessage> ExecuteAsync(WebhookPayload payload)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(payload), new MediaTypeHeaderValue("application/json"))
            };
            return await _client.SendAsync(request);
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}