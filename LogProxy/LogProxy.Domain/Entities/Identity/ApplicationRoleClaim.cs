using Microsoft.AspNetCore.Identity;

namespace LogProxy.Domain.Entities.Identity
{
    public class ApplicationRoleClaim: IdentityRoleClaim<int>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}
