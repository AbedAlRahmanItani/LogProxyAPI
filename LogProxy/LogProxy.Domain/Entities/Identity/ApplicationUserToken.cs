using Microsoft.AspNetCore.Identity;

namespace LogProxy.Domain.Entities.Identity
{
    public class ApplicationUserToken: IdentityUserToken<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}