using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsImergencyMessageConfig : IEntityTypeConfiguration<MrsImergencyMessage>
    {
        public void Configure(EntityTypeBuilder<MrsImergencyMessage> builder)
        {
            builder
                .HasOne(x => x.Location)
                .WithOne(x => x.ImergencyMessage)
                .HasForeignKey<MrsImergencyMessage>(f => f.LocationId);

                
        }
    }
}
