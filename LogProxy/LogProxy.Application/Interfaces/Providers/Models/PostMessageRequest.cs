using Newtonsoft.Json;

namespace LogProxy.Application.Interfaces.Providers.Models
{
    public class PostMessageRequest
    {
        [JsonProperty("records")]
        public Record[] Records { get; set; }

        public class Record
        {
            [JsonProperty("fields")]
            public MessageField Fields { get; set; }
        }
    }
}
