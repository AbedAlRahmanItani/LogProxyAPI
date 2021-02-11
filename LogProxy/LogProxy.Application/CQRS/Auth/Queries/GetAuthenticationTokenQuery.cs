using MediatR;
using LogProxy.Application.CQRS.Auth.Models;

namespace LogProxy.Application.CQRS.Auth.Queries
{
    public class GetAuthenticationTokenQuery : IRequest<AuthenticationToken>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}