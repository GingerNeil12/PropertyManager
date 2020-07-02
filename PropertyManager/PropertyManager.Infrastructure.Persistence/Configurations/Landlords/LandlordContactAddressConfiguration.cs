using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Infrastructure.Persistence.Configurations.Landlords
{
    internal class LandlordContactAddressConfiguration : IEntityTypeConfiguration<LandlordContactAddress>
    {
        public void Configure(EntityTypeBuilder<LandlordContactAddress> builder)
        {
            builder.ToTable("ContactAddress", "Landlord");

            builder.Property(x => x.HouseNumber)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Street)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.PostalArea)
                .HasMaxLength(256);

            builder.Property(x => x.PostalTown)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.County)
                .HasMaxLength(256);

            builder.Property(x => x.Country)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Postcode)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.LandlordId)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRowVersion();
        }
    }
}
