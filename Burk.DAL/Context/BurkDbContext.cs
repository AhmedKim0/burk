using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Entity;
using Burk.DAL.Entity.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Context
{
    public class BurkDbContext : IdentityDbContext
    {
        public BurkDbContext(DbContextOptions<BurkDbContext> options) : base(options)
        {
        }
		//public override int SaveChanges(bool acceptAllChangesOnSuccess)
		//{
		//	OnBeforeSaving();
		//	return base.SaveChanges(acceptAllChangesOnSuccess);
		//}

		//public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		//{
		//	OnBeforeSaving();
		//	return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		//}
		//private void OnBeforeSaving()
		//{
		//	foreach (var entry in ChangeTracker.Entries())
		//	{
		//		switch (entry.State)
		//		{
		//			case EntityState.Added:
		//				SetCreatedPropertiesForAuditableEntities(entry);
		//				if (entry.Entity is not IHardDeletable)
		//				{
		//					entry.CurrentValues["IsDeleted"] = false;
		//				}
		//				break;
		//			case EntityState.Modified:
		//				SetUpdatedPropertiesForAuditableEntities(entry);
		//				break;
		//			case EntityState.Deleted:
		//				SetUpdatedPropertiesForAuditableEntities(entry);
		//				if (entry.Entity is not IHardDeletable)
		//				{
		//					entry.State = EntityState.Modified;
		//					entry.CurrentValues["IsDeleted"] = true;
		//				}
		//				break;
		//		}
		//	}
			
		//}

		//private static void SetUpdatedPropertiesForAuditableEntities(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
		//{
		//	if (entry.Entity is IAuditable)
		//	{
		//		((IAuditable)entry.Entity).UpdatedAtUtc = DateTime.UtcNow;
		//		((IAuditable)entry.Entity).UpdatedBy = ((IAuditable)entry.Entity).UpdatedBy ?? null;
		//	}
		//}

		//private static void SetCreatedPropertiesForAuditableEntities(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
		//{
		//	if (entry.Entity is IAuditable)
		//	{
		//		((IAuditable)entry.Entity).CreatedAtUtc = DateTime.UtcNow;
		//		((IAuditable)entry.Entity).CreatedBy = ((IAuditable)entry.Entity).CreatedBy == null ? Guid.Empty : ((IAuditable)entry.Entity).CreatedBy;
		//	}
		//}
		//protected override void OnModelCreating(ModelBuilder builder)
		//{

		//	base.OnModelCreating(builder);
		//}

		public DbSet<Client> Clients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RecordedVisit> RecordedVisits { get; set; }
        public DbSet<WaitingList> WaitingLists { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Question> Questions { get; set; }



	}

    
 }
