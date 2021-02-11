using Microsoft.AspNetCore.Identity;

namespace LogProxy.Domain.Entities.Identity
{
    public class ApplicationUserClaim: IdentityUserClaim<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
