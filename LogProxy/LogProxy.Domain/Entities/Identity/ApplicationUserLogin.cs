using Microsoft.AspNetCore.Identity;

namespace LogProxy.Domain.Entities.Identity
{
    public class ApplicationUserLogin: IdentityUserLogin<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}