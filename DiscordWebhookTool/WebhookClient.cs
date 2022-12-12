// MIT License
//
// Copyright(c) 2022 Bujju
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordWebhookTool
{
    /// <summary>
    /// A client for executing Discord webhooks.
    /// </summary>
    public class WebhookClient : IDisposable
    {
        /// <summary>
        /// The client used to send REST requests.
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookClient"/> class.
        /// </summary>
        /// <param name="webhookUrl">The webhook URL.</param>
        public WebhookClient(string webhookUrl)
        {
            _client = new HttpClient();
            WebhookUrl = webhookUrl;
        }

        /// <summary>
        /// Gets or sets the webhook URL.
        /// </summary>
        public string WebhookUrl
        {
            get => _client.BaseAddress.ToString();
            set => _client.BaseAddress = new Uri(value);
        }

        /// <summary>
        /// Executes the webhook.
        /// </summary>
        /// <param name="payload">The payload to use.</param>
        /// <returns>The response returned by the REST request.</returns>
        public async Task<HttpResponseMessage> ExecuteAsync(WebhookPayload payload)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(payload), new MediaTypeHeaderValue("application/json"))
            };
            return await _client.SendAsync(request);
        }

        /// <summary>
        /// Disposes of this <see cref="WebhookClient"/>.
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}