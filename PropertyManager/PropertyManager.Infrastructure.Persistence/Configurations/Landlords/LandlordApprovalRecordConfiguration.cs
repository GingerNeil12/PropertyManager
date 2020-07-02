using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManager.Domain.Enums;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Infrastructure.Persistence.Configurations.Landlords
{
    internal class LandlordApprovalRecordConfiguration : IEntityTypeConfiguration<LandlordApprovalRecord>
    {
        public void Configure(EntityTypeBuilder<LandlordApprovalRecord> builder)
        {
            builder.ToTable("ApprovalRecord", "Landlord");

            builder.Property(x => x.ApprovalStatus)
                .IsRequired()
                .HasConversion(
                    x => x.ToString(),
                    x => (ApprovalStatus)Enum.Parse(typeof(ApprovalStatus), x));

            builder.Property(x => x.SubmittedOn)
                .IsRequired();

            builder.Property(x => x.LandlordId)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRowVersion();
        }
    }
}
