using LogProxy.Application.Providers.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.Interfaces.Providers
{
    public interface IAirtableApiClient
    {
        Task<MessagesResponse> GetMessagesAsync(GetMessagesRequest request, CancellationToken cancellationToken);
        Task<MessagesResponse> CreateMessagesAsync(PostMessagesRequest request, CancellationToken cancellationToken);
    }
}