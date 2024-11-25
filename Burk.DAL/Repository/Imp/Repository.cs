using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Context;
using Burk.DAL.Entity.Common;
using Burk.DAL.Repository.Helper;
using Burk.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Repository.Imp
{
    public class Repository<T>:IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly BurkDbContext _dbContext;

        /// <summary>
        /// Constructor that initializes the DbContext used for database operations.
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public Repository(BurkDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Adds a new entity to the database asynchronously.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="saveChanges">Flag to save changes immediately</param>
        /// <returns>Added entity</returns>
        public async Task<T> AddAsync(T entity, bool saveChanges = true)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (saveChanges)
                await SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Adds a range of entities to the database asynchronously.
        /// </summary>
        /// <param name="entities">Collection of entities to add</param>
        /// <param name="saveChanges">Flag to save changes immediately</param>
        /// <returns>Collection of added entities</returns>
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            if (saveChanges)
                await SaveChangesAsync();

            return entities;
        }

        /// <summary>
        /// Checks if any entity satisfies the provided expression asynchronously.
        /// </summary>
        /// <param name="expression">Condition to check</param>
        /// <returns>True if any entity satisfies the condition, otherwise false</returns>
        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
            => _dbContext.Set<T>().AnyAsync(expression);

        /// <summary>
        /// Counts the entities that satisfy the provided expression asynchronously.
        /// </summary>
        /// <param name="expression">Condition to count entities</param>
        /// <returns>Count of entities</returns>
        public Task<int> CountAsync(Expression<Func<T, bool>> expression)
            => _dbContext.Set<T>().CountAsync(expression);

        /// <summary>
        /// Counts all entities in the database asynchronously.
        /// </summary>
        /// <returns>Count of all entities</returns>
        public Task<int> CountAllAsync()
            => _dbContext.Set<T>().CountAsync();

        /// <summary>
        /// Deletes a given entity from the database asynchronously.
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="saveChanges">Flag to save changes immediately</param>
        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (saveChanges)
                await SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a range of entities from the database asynchronously.
        /// </summary>
        /// <param name="entities">Collection of entities to delete</param>
        /// <param name="saveChanges">Flag to save changes immediately</param>
        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            if (saveChanges)
                await SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the first entity that matches the provided expression asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="expression">Condition to match</param>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>The first matching entity</returns>
        public Task<T> FirstAsync(Expression<Func<T, bool>> expression, bool enableTracking = true)
               => enableTracking
                   ? _dbContext.Set<T>().FirstAsync(expression)
                   : _dbContext.Set<T>().AsNoTracking().FirstAsync(expression);

        /// <summary>
        /// Retrieves the first entity or null asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>The first entity or null</returns>
        public Task<T?> FirstOrDefaultAsync(bool enableTracking = true)
            => enableTracking
                ? _dbContext.Set<T>().FirstOrDefaultAsync()
                : _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync();

        /// <summary>
        /// Retrieves the first entity that matches the provided expression or null asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="expression">Condition to match</param>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>The first matching entity or null</returns>
        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool enableTracking = true)
            => enableTracking
                ? _dbContext.Set<T>().FirstOrDefaultAsync(expression)
                : _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);

        /// <summary>
        /// Retrieves the last entity or null asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>The last entity or null</returns>
        public Task<T?> LastOrDefaultAsync(bool enableTracking = true)
            => enableTracking
                ? _dbContext.Set<T>().LastOrDefaultAsync()
                : _dbContext.Set<T>().AsNoTracking().LastOrDefaultAsync();

        /// <summary>
        /// Retrieves the last entity that matches the provided expression or null asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="expression">Condition to match</param>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>The last matching entity or null</returns>
        public Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> expression, bool enableTracking = true)
            => enableTracking
                ? _dbContext.Set<T>().OrderBy(x => x.Id).LastOrDefaultAsync(expression)
                : _dbContext.Set<T>().AsNoTracking().OrderBy(x => x.Id).LastOrDefaultAsync(expression);

        /// <summary>
        /// Retrieves an entity by its integer ID asynchronously.
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Matching entity or null</returns>
        public async Task<T?> GetByIdAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        /// <summary>
        /// Retrieves an entity by its string ID asynchronously.
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Matching entity or null</returns>
        public async Task<T?> GetByIdAsync(string id)
            => await _dbContext.Set<T>().FindAsync(id);

        /// <summary>
        /// Gets a queryable set of entities.
        /// </summary>
        /// <returns>Queryable set of entities</returns>
        public IQueryable<T> GetQuery()
            => _dbContext.Set<T>().AsQueryable();

        /// <summary>
        /// Gets the DbSet of the current entity type.
        /// </summary>
        /// <returns>DbSet of the entity type</returns>
        public DbSet<T> GetDbSet()
            => _dbContext.Set<T>();

        /// <summary>
        /// Lists all entities asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>List of all entities</returns>
        public Task<List<T>> ListAllAsync(bool enableTracking = true)
            => enableTracking
                ? _dbContext.Set<T>().ToListAsync()
                : _dbContext.Set<T>().AsNoTracking().ToListAsync();

        /// <summary>
        /// Lists entities matching the provided expression asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="expression">Condition to match</param>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>List of matching entities</returns>
        public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, bool enableTracking = true)
            => enableTracking
                ? _dbContext.Set<T>().Where(expression).ToListAsync()
                : _dbContext.Set<T>().AsNoTracking().Where(expression).ToListAsync();

        /// <summary>
        /// Lists paged entities matching the provided expression asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="expression">Condition to match</param>
        /// <param name="pageNo">Page number</param>
        /// <param name="pageSize">Size of each page</param>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <returns>Paged list of matching entities</returns>
        //public Task<PagedList<T>> ListPagedAsync(Expression<Func<T, bool>> expression, int pageNo, int pageSize, bool enableTracking = true)
        //    => enableTracking
        //        ? Task.FromResult(_dbContext.Set<T>().Where(expression).ToPagedList(pageNo, pageSize))
        //        : Task.FromResult(_dbContext.Set<T>().AsNoTracking().Where(expression).ToPagedList(pageNo, pageSize));

        /// <summary>
        /// Saves changes to the database asynchronously.
        /// </summary>
        /// <returns>Number of affected rows</returns>
        public Task<int> SaveChangesAsync()
            => _dbContext.SaveChangesAsync();

        /// <summary>
        /// Updates an entity in the database asynchronously.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="saveChanges">Flag to save changes immediately</param>
        /// <returns>Updated entity</returns>
        public async Task<T> UpdateAsync(T entity, bool saveChanges = true)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (saveChanges)
                await SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Updates a range of entities in the database asynchronously.
        /// </summary>
        /// <param name="entities">Collection of entities to update</param>
        /// <param name="saveChanges">Flag to save changes immediately</param>
        /// <returns>Updated entities</returns>
        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            if (saveChanges)
                await SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Lists entities matching the provided expression with optional sorting asynchronously.
        /// Optionally enables or disables entity tracking.
        /// </summary>
        /// <param name="expression">Condition to match</param>
        /// <param name="enableTracking">Flag to enable or disable tracking</param>
        /// <param name="sortExpression">Expression to sort by</param>
        /// <param name="sortType">Sort direction (ASC or DESC)</param>
        /// <returns>List of matching entities with optional sorting</returns>
        public async Task<List<T>> ListWithExpressionAsync(Expression<Func<T, bool>> expression, bool enableTracking = true, Expression<Func<T, object>>? sortExpression = null, string sortType = "DESC")
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(expression);

            if (!enableTracking)
                query = query.AsNoTracking();

            if (sortExpression != null)
            {
                query = sortType.Equals("DESC", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(sortExpression)
                    : query.OrderBy(sortExpression);
            }

            return await query.ToListAsync();
        }
    }
}
