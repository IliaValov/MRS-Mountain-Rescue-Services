using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsUserDeviceConfig : IEntityTypeConfiguration<MrsUserDevice>
    {
        public void Configure(EntityTypeBuilder<MrsUserDevice> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
