using System;
using Newtonsoft.Json;

namespace LogProxy.Application.Interfaces.Providers.Models
{
    public class GetMessagesResponse
    {
        [JsonProperty("records")]
        public Record[] Records { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }

        public class Record
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("fields")]
            public MessageField Fields { get; set; }

            [JsonProperty("createdTime")]
            public DateTimeOffset CreatedTime { get; set; }
        }
    }
}
