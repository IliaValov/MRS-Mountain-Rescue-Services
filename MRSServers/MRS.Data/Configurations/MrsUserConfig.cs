using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    class MrsUserConfig : IEntityTypeConfiguration<MrsUser>
    {
        public void Configure(EntityTypeBuilder<MrsUser> builder)
        {
            builder
                .HasOne(x => x.AuthanticationToken)
                .WithOne(x => x.User)
                .HasForeignKey<MrsUser>(f => f.AuthanticationTokenId);

        }
    }
}
