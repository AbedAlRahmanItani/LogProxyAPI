using MediatR;
using Microsoft.Extensions.Options;
using LogProxy.Application.CQRS.Auth.Models;
using LogProxy.Application.Options;
using LogProxy.Application.Services.Security;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.CQRS.Auth.Queries
{
    public class GetAuthenticationTokenQueryHandler : IRequestHandler<GetAuthenticationTokenQuery, AuthenticationToken>
    {
        private readonly IAuthService _authService;
        private readonly AuthenticationOptions _appSettings;

        public GetAuthenticationTokenQueryHandler(IAuthService authService, IOptions<AuthenticationOptions> appSettings)
        {
            _authService = authService;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticationToken> Handle(GetAuthenticationTokenQuery request, CancellationToken cancellationToken)
        {
            var expiryDate = DateTime.UtcNow.AddDays(1); // TODO: Move nbr of days to appSettings

            var token = await _authService.GenerateJwtToken(request.UserName, request.Password, _appSettings.JwtSecret, expiryDate, cancellationToken).ConfigureAwait(false);

            return new AuthenticationToken
            {
                Token = token,
                ExpiryDate = expiryDate
            };
        }
    }
}
