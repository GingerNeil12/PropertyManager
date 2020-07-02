using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Enums;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Infrastructure.Persistence.Configurations.Landlords
{
    internal class LandlordConfiguration : IEntityTypeConfiguration<Landlord>
    {
        public void Configure(EntityTypeBuilder<Landlord> builder)
        {
            builder.ToTable("Landlord", "Landlord");

            builder.Property(x => x.Title)
                .HasMaxLength(10);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.MiddleNames)
                .HasMaxLength(256);

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.MobilePhone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.HomePhone)
                .HasMaxLength(20);

            builder.Property(x => x.Dob)
                .IsRequired();

            builder.Property(x => x.RegsiterNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ActiveStatus)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => (ActiveStatus)Enum.Parse(typeof(ActiveStatus), x));

            builder.Property(x => x.Timestamp)
                .IsRowVersion();
        }
    }
}
