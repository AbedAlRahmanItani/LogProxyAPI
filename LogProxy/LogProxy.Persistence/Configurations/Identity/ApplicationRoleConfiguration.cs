using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LogProxy.Domain.Entities.Identity;

namespace LogProxy.Persistence.Configurations.Identity
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("Roles", "identity");
            builder.Property(role => role.Id).UseIdentityColumn();
            builder.Property(role => role.Name).IsRequired().HasMaxLength(255);

            builder.HasMany(user => user.UserRoles).WithOne(user => user.Role).HasForeignKey(role => role.RoleId);
            builder.HasMany(role => role.RoleClaims).WithOne(claim => claim.Role).HasForeignKey(role => role.RoleId);
        }
    }
}