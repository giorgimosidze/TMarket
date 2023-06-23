using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMarket.Persistence.DbModels.Interfaces;
using TMarket.Persistence.Repositories.Abstract;

namespace TMarket.Persistence.Repositories.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IDbEntity
    {
        protected MarketDbContext _context;
        private DbSet<T> _dbset;

        public BaseRepository(MarketDbContext ctx)

        {
            _context = ctx;
            _dbset = _context.Set<T>();
        }

        public async Task<T> DeleteAsync(object id)
        {
            var result = await GetByIdAsync(id);
            result.IsDeleted = true;
            await SaveAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithNoTracking()
        {
            return await NotDeletedRowsOnly().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithTracking()
        {
            return await NotDeletedRowsOnly().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var item = await _dbset.FindAsync(id);

            return item == null ? null
                : item.IsDeleted ? null : item;
        }

        public async Task<T> InsertAsync(T obj)
        {
            await _dbset.AddAsync(obj);
            await SaveAsync();
            return obj;
        }

        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await SaveAsync();
            return obj;
        }

        private IQueryable<T> NotDeletedRowsOnly()
        {
            return _dbset.Where(t => t.IsDeleted == false).AsQueryable();
        }

        public IEnumerable<T> GetAll<TResult>(Expression<Func<T, TResult>> selector,
                                                  Expression<Func<T, bool>> predicate = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                  bool disableTracking = true)
        {
            IQueryable<T> query = _dbset;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return (IEnumerable<T>)orderBy(query).Select(selector);
            }
            else
            {
                return (IEnumerable<T>)query.Select(selector);
            }
        }
    }
}
