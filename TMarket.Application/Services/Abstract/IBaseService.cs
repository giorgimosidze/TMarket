using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Application.Services.Abstract
{
    public interface IBaseService<T> : IService where T : class, IDbEntity
    {
        Task<IEnumerable<T>> GetAllAsyncWithNoTracking();
        Task<IEnumerable<T>> GetPaginatedResultAsyncAsNoTracking(int currentPage, int pageSize, string sortBy, bool isAsc);
        IEnumerable<T> FindAllAsyncWithNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<T> GetByIdAsync(object id);
        Task<T> InsertAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task<T> DeleteAsync(object id);

    }
}
