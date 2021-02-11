using AutoMapper;
using LogProxy.Application.CQRS.Messages.Models;
using LogProxy.Application.Interfaces.Providers;
using LogProxy.Application.Providers.Models;
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
            if (request.Messages.Any())
                throw new ApplicationException("Atleast on Message should be available in the request");

            var postRequest = new PostMessagesRequest();
            foreach(var msg in request.Messages)
            {
                var records = new List<PostMessagesRequest.Record>
                {
                    new PostMessagesRequest.Record
                    {
                        Fields = new MessageField
                        {
                            Id = msg.Id,
                            Summary = msg.Title,
                            Message = msg.Text,
                            ReceivedAt = msg.ReceivedAt
                        }
                    }
                };
                postRequest.Records = records.ToArray();
            }

            var response = await _airtableApiClient.CreateMessagesAsync(postRequest, cancellationToken);

            return response.Records.Select(record => _mapper.Map<MessagesViewModel>(record));
        }
    }
}
