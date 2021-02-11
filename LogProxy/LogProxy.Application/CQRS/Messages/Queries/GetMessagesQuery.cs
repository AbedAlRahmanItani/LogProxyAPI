using LogProxy.Application.CQRS.Messages.Models;
using MediatR;
using System.Collections.Generic;

namespace LogProxy.Application.CQRS.Messages.Queries
{
    public class GetMessagesQuery : IRequest<IEnumerable<MessagesViewModel>>
    {
        public int MaxRecords { get; set; }
        public string View { get; set; }
    }
}
