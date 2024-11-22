using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Repository.Helper;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Repository.Interface
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, bool saveChanges = true);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAllAsync();
        Task DeleteAsync(T entity, bool saveChanges = true);
        Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        Task<T?> FirstAsync(Expression<Func<T, bool>> expression, bool enableTracking = true);
        Task<T?> FirstOrDefaultAsync(bool enableTracking = true);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool enableTracking = true);
        Task<T?> LastOrDefaultAsync(bool enableTracking = true);
        Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> expression, bool enableTracking = true);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(string id);
        IQueryable<T> GetQuery();
        DbSet<T> GetDbSet();
        Task<List<T>> ListAllAsync(bool enableTracking = true);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, bool enableTracking = true);
        //Task<PagedList<T>> ListPagedAsync(Expression<Func<T, bool>> expression, int pageNo, int pageSize, bool enableTracking = true);
        Task<int> SaveChangesAsync();
        Task<T> UpdateAsync(T entity, bool saveChanges = true);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        Task<List<T>> ListWithExpressionAsync(Expression<Func<T, bool>> expression, bool enableTracking = true, Expression<Func<T, object>>? sortExpression = null, string sortType = "DESC");
    }
}

