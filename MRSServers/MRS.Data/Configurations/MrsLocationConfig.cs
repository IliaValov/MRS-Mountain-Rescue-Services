using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsLocationConfig : IEntityTypeConfiguration<MrsLocation>
    {
        public void Configure(EntityTypeBuilder<MrsLocation> builder)
        {
            builder
                .HasOne(x => x.ImergencyMessage)
                .WithOne(x => x.Location);
        }
    }
}
