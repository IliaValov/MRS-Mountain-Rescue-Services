using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRS.Models;

namespace MRS.Data.Configurations
{
    public class MrsUserVerificationConfig : IEntityTypeConfiguration<MrsUserVerification>
    {
        public void Configure(EntityTypeBuilder<MrsUserVerification> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
