using AutoMapper;
using LogProxy.Application.CQRS.Messages.Models;
using LogProxy.Application.Interfaces.Providers;
using LogProxy.Application.Providers.Airtable.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.CQRS.Messages.Queries
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessagesViewModel>>
    {
        private readonly IAirtableApiClient _airtableApiClient;
        private readonly IMapper _mapper;

        public GetMessagesQueryHandler(IAirtableApiClient airtableApiClient, IMapper mapper)
        {
            _airtableApiClient = airtableApiClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessagesViewModel>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var response = await _airtableApiClient.GetMessagesAsync(
                new GetMessagesRequest
                {
                    MaxRecords = request.MaxRecords,
                    View = request.View
                }, cancellationToken).ConfigureAwait(false);

            return response.Records.Select(record => _mapper.Map<MessagesViewModel>(record));
        }
    }
}