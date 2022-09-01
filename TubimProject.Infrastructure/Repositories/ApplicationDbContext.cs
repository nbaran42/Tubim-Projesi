using AspNetCoreHero.Abstractions.Domain;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Context;
using TubimProject.Application.Interfaces.Shared;
using TubimProject.Domain.Entities.Common;
using TubimProject.Domain.Entities.Logging;

namespace TubimProject.Infrastructure.Repositories
{
    public partial class ApplicationDbContext : AuditDbContext, IApplicationDbContext
    {

        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {

            _authenticatedUser = authenticatedUser;
        }


       

        public DbSet<ERRORLOGS> ERRORLOGS { get; set; }
        public DbSet<UT_AUDIT> UT_AUDIT { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<UT_CRASHREPORT> UT_CRASHREPORT { get; set; }
        public DbSet<UT_NOTIFICATION> UT_NOTIFICATION { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _authenticatedUser.Username;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _authenticatedUser.Username;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
        }
    }
}