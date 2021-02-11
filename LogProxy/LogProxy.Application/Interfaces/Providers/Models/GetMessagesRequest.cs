namespace LogProxy.Application.Interfaces.Providers.Models
{
    public class GetMessagesRequest
    {
        public int MaxRecords { get; set; }

        public string View { get; set; }
    }
}
