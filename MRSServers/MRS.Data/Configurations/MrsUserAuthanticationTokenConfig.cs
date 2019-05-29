using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsUserAuthanticationTokenConfig : IEntityTypeConfiguration<MrsUserAuthanticationToken>
    {
        public void Configure(EntityTypeBuilder<MrsUserAuthanticationToken> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.AuthanticationToken)
                .HasForeignKey<MrsUserAuthanticationToken>(f => f.UserId);
        }
    }
}
