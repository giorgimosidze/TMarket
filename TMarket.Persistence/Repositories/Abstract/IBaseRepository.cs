using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.Repositories.Abstract
{
    public interface IBaseRepository<T> where T: class, IDbEntity
    {
        Task<IEnumerable<T>> GetAllAsyncWithNoTracking();
        Task<IEnumerable<T>> GetAllAsyncWithTracking();
        IEnumerable<T> GetAll<TResult>(Expression<Func<T, TResult>> selector,
                                    Expression<Func<T, bool>> predicate,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                                    bool disableTracking);
        Task<T> GetByIdAsync(object id);
        Task<T> InsertAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task<T> DeleteAsync(object id);
    }
}
