using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Application.Common.Interfaces;
using PropertyManager.Domain.Common;
using PropertyManager.Domain.Models.Landlords;
using PropertyManager.Infrastructure.Persistence.Extensions;
using PropertyManager.Infrastructure.Security.Models;

namespace PropertyManager.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<LandlordActivity> LandlordActivities { get; set; }
        public DbSet<LandlordApprovalRecord> LandlordApprovalRecords { get; set; }
        public DbSet<LandlordContactAddress> LandlordContactAddresses { get; set; }
        public DbSet<LandlordNote> LandlordNotes { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUser currentUser,
            IDateTime dateTime)
            : base(options)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.RemovePluralTableNames();
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.UserId;
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUser.UserId;
                        entry.Entity.UpdatedOn = _dateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
