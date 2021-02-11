using LogProxy.Application.CQRS.Messages.Models;
using MediatR;
using System.Collections.Generic;

namespace LogProxy.Application.CQRS.Messages.Commands
{
    public class CreateMessagesCommand : IRequest<IEnumerable<MessagesViewModel>>
    {
        public List<MessagesViewModel> Messages { get; set; }
    }
}