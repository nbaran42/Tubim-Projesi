using AspNetCoreHero.Abstractions.Domain;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Context;
using TubimProject.Application.Interfaces.Shared;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories
{
    public class KurumlarContext : AuditDbContext, IKurumlarContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public KurumlarContext(DbContextOptions<KurumlarContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _authenticatedUser=authenticatedUser;
        }

        public DbSet<UT_OLAY> UT_OLAY { get; set; }
        public DbSet<UT_OLAYDETAY> UT_OLAYDETAY { get; set; }
        public DbSet<UT_MADDE> UT_MADDE { get; set; }
        public DbSet<KT_MADDETURLERI> KT_MADDETURLERI { get; set; }
        public DbSet<KT_KURUMLAR> KT_KURUMLAR { get; set; }
        public DbSet<UT_SAHIS> UT_SAHISLAR { get; set; }
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
