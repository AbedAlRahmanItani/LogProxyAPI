using System;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.Services.Security
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string username, string password, string jwtSecret, DateTime expiryDate, CancellationToken cancellationToken);
    }
}