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

using System.Text.Json.Serialization;

namespace DiscordWebhookTool.Entities
{
    /// <summary>
    /// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure">See documentation</see>
    /// </summary>
    public class EmbedVideo
    {
        /// <summary>
        /// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure">See documentation</see>
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure">See documentation</see>
        /// </summary>
        [JsonPropertyName("proxy_url")]
        public string ProxyUrl { get; set; }

        /// <summary>
        /// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure">See documentation</see>
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        /// <summary>
        /// <see href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure">See documentation</see>
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; set; }
    }
}