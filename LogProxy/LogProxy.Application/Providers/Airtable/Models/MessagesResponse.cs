using Newtonsoft.Json;
using System;

namespace LogProxy.Application.Providers.Airtable.Models
{
    public class MessagesResponse
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
            public DateTime CreatedTime { get; set; }
        }
    }
}