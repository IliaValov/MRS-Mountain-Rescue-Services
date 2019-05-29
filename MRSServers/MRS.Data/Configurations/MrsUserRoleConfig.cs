using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsUserRoleConfig : IEntityTypeConfiguration<MrsUserRole>
    {
        public void Configure(EntityTypeBuilder<MrsUserRole> builder)
        {
            builder
                .HasKey(x => new { x.UserId, x.RoleId });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles);

            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles);
        }
    }
}
