namespace LogProxy.Application.Providers.Airtable.Models
{
    public class GetMessagesRequest
    {
        public int MaxRecords { get; set; }

        public string View { get; set; }
    }
}
