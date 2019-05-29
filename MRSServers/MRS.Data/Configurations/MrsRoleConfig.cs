using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsRoleConfig : IEntityTypeConfiguration<MrsRole>
    {
        public void Configure(EntityTypeBuilder<MrsRole> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
