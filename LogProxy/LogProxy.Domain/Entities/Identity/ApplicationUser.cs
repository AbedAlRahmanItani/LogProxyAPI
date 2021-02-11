using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LogProxy.Domain.Entities.Identity
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string FullName { get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }

        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }

        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }

        public virtual ICollection<ApplicationUserRole> Roles { get; set; }
    }
}
