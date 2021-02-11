using LogProxy.Application.Interfaces.Providers.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.Interfaces.Providers
{
    public interface IAirtableApiClient
    {
        Task<GetMessagesResponse> GetMessagesAsync(GetMessagesRequest request, CancellationToken cancellationToken);
    }
}