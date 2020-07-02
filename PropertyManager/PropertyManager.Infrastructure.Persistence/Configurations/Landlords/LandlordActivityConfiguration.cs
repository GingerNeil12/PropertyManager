using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Infrastructure.Persistence.Configurations.Landlords
{
    internal class LandlordActivityConfiguration : IEntityTypeConfiguration<LandlordActivity>
    {
        public void Configure(EntityTypeBuilder<LandlordActivity> builder)
        {
            builder.ToTable("Activity", "Landlord");

            builder.Property(x => x.Action)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.HappenedOn)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.LandlordId)
                .IsRequired();
        }
    }
}
