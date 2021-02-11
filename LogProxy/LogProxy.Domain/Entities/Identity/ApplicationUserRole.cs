﻿using Microsoft.AspNetCore.Identity;

namespace LogProxy.Domain.Entities.Identity
{
    public class ApplicationUserRole: IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}