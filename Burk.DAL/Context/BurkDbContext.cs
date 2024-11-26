using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Burk.DAL.Context.contextIdentity;
using Burk.DAL.Entity;
using Burk.DAL.Entity.Common;
using Burk.DAL.Entity.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Context
{
    public class BurkDbContext : IdentityDbContext
    {
		private readonly IUserContextService _userContextService;
		public BurkDbContext(DbContextOptions<BurkDbContext> options, IUserContextService userContextService) : base(options)
        {
			_userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
		}
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			OnBeforeSaving();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			OnBeforeSaving();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
		private void OnBeforeSaving()
		{
			var userId = _userContextService.GetCurrentUserId();
			foreach (var entry in ChangeTracker.Entries())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						SetCreatedPropertiesForAuditableEntities(entry,userId);
						//if (entry.Entity is not IHardDeletable)
						//{
						//	entry.CurrentValues["IsDeleted"] = false;
						//}
						break;
					case EntityState.Modified:
						SetUpdatedPropertiesForAuditableEntities(entry,userId);
						break;
					case EntityState.Deleted:
						SetUpdatedPropertiesForAuditableEntities(entry, userId);
						//if (entry.Entity is not IHardDeletable)
						//{
						//	entry.State = EntityState.Modified;
						//	entry.CurrentValues["IsDeleted"] = true;
						//}
						break;
				}
			}

		}

		private static void SetUpdatedPropertiesForAuditableEntities(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry,Guid userId)
		{
			
			if (entry.Entity is IAuditable)
			{
				((IAuditable)entry.Entity).UpdatedAtUtc = DateTime.UtcNow;
				((IAuditable)entry.Entity).UpdatedBy = ((IAuditable)entry.Entity).UpdatedBy ?? userId;
			}
		}

		private static void SetCreatedPropertiesForAuditableEntities(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, Guid userId)
		{

			if (entry.Entity is IAuditable)
			{
				((IAuditable)entry.Entity).CreatedAtUtc = DateTime.UtcNow;
				((IAuditable)entry.Entity).CreatedBy = ((IAuditable)entry.Entity).CreatedBy == null? userId : ((IAuditable)entry.Entity).CreatedBy;
			}
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{

			base.OnModelCreating(builder);
			//builder.Entity<ApplicationUserRole>(entity =>
			//{
			//	entity.Property(e => e.IsDeleted).HasDefaultValue(false);
			//});

			////add isDeleted query filter to all entities
			//foreach (var entityType in builder.Model.GetEntityTypes().Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity))))
			//{
			//	var parameter = Expression.Parameter(entityType.ClrType);
			//	var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
			//	var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
			//	BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));
			//	var lambda = Expression.Lambda(compareExpression, parameter);
			//	builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
			//}
		}

		public DbSet<Client> Clients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RecordedVisit> RecordedVisits { get; set; }
        public DbSet<WaitingList> WaitingLists { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Question> Questions { get; set; }



	}

    
 }
