using System.Text.Json.Serialization;

namespace DiscordWebhookTool
{
    public class WebhookPayload
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}