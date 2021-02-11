using Newtonsoft.Json;

namespace LogProxy.Application.Providers.Models
{
    public class PostMessagesRequest
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
