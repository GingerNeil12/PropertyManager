using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Domain.Models.Landlords;

namespace PropertyManager.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Landlord> Landlords { get; set; }
        DbSet<LandlordActivity> LandlordActivities { get; set; }
        DbSet<LandlordApprovalRecord> LandlordApprovalRecords { get; set; }
        DbSet<LandlordContactAddress> LandlordContactAddresses { get; set; }
        DbSet<LandlordNote> LandlordNotes { get; set; }

        Task<int> SaveChangesAsync();
    }
}
