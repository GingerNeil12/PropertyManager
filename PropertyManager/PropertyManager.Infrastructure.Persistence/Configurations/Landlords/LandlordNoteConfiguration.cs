using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Infrastructure.Persistence.Configurations.Landlords
{
    internal class LandlordNoteConfiguration : IEntityTypeConfiguration<LandlordNote>
    {
        public void Configure(EntityTypeBuilder<LandlordNote> builder)
        {
            builder.ToTable("Note", "Landlord");

            builder.Property(x => x.Summary)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.LandlordId)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRowVersion();
        }
    }
}
