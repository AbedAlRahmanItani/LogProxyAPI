using AutoMapper;
using LogProxy.Application.CQRS.Messages.Models;
using LogProxy.Application.Interfaces.Providers;
using LogProxy.Application.Providers.Airtable.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.CQRS.Messages.Commands
{
    public class CreateMessagesCommandHandler : IRequestHandler<CreateMessagesCommand, IEnumerable<MessagesViewModel>>
    {
        private readonly IAirtableApiClient _airtableApiClient;
        private readonly IMapper _mapper;

        public CreateMessagesCommandHandler(IAirtableApiClient airtableApiClient, IMapper mapper)
        {
            _airtableApiClient = airtableApiClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessagesViewModel>> Handle(CreateMessagesCommand request, CancellationToken cancellationToken)
        {
            if (!request.Messages.Any())
                throw new ApplicationException("Atleast on Message should be available in the request");

            var postMessagesRequest = new PostMessagesRequest
            {
                Records = (from msg in request.Messages
                           select new PostMessagesRequest.Record
                           {
                               Fields = new MessageField
                               {
                                   Id = msg.Id,
                                   Summary = msg.Title,
                                   Message = msg.Text,
                                   ReceivedAt = msg.ReceivedAt
                               }
                           }).ToArray()
            };

            var response = await _airtableApiClient.CreateMessagesAsync(postMessagesRequest, cancellationToken);

            return response.Records.Select(record => _mapper.Map<MessagesViewModel>(record));
        }
    }
}
