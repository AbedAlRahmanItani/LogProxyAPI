using LogProxy.Domain.Entities.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Application.Services.Security
{
    public interface IUserClaimService
    {
        Task<IEnumerable<Claim>> GetClaims(ApplicationUser user, CancellationToken cancellationToken);
    }
}