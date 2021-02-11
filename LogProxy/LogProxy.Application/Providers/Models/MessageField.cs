using Newtonsoft.Json;
using System;

namespace LogProxy.Application.Providers.Models
{
    public class MessageField
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("receivedAt")]
        public DateTime? ReceivedAt { get; set; }
    }
}
