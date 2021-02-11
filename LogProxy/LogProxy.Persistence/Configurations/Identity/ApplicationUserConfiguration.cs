using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LogProxy.Domain.Entities.Identity;

namespace LogProxy.Persistence.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users", "identity");
            builder.Property(user => user.FullName).IsRequired().HasMaxLength(255);
            builder.Property(user => user.UserName).IsRequired();
            builder.HasIndex(user => user.UserName).IsUnique();
            builder.Property(user => user.Email).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasMany(user => user.Claims).WithOne(userClaim => userClaim.User).HasForeignKey(userClaim => userClaim.UserId);
            builder.HasMany(user => user.Logins).WithOne(userLogin => userLogin.User).HasForeignKey(userLogin => userLogin.UserId);
            builder.HasMany(user => user.Tokens).WithOne(userToken => userToken.User).HasForeignKey(userToken => userToken.UserId);
            builder.HasMany(user => user.Roles).WithOne(userRole => userRole.User).HasForeignKey(userRole => userRole.UserId);
        }
    }
}
