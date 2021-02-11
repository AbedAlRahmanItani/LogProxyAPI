using Microsoft.EntityFrameworkCore;
using LogProxy.Application.Services.Security;
using LogProxy.Domain.Entities.Identity;
using LogProxy.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Infrastructure.Services.Security
{
    public class UserClaimService : IUserClaimService
    {
        private readonly ApplicationDbContext dbContext;

        public UserClaimService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user, CancellationToken cancellationToken)
        {
            var applicationUserClaims = await Queryable.Where(dbContext.Set<ApplicationUserClaim>(), r => r.UserId == user.Id)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            var userClaims = applicationUserClaims.Select(claim => claim.ToClaim());

            var userRoles = await Queryable.Where(dbContext.Set<ApplicationUserRole>(), r => r.UserId == user.Id)
                .Include(userRole => userRole.Role)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            var roleClaims = userRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name));

            var identityClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.Name, user.UserName.ToString(), ClaimValueTypes.String),
            };
            identityClaims.AddRange(userClaims);
            identityClaims.AddRange(roleClaims);

            return identityClaims;
        }
    }
}
