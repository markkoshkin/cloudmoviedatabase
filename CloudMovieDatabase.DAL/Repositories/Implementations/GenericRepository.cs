using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CloudMovieDatabase.DAL.Repositories.Abstractions
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CloudMovieDatabaseContext _dbContext;

        public GenericRepository(CloudMovieDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> AllAsync(int skip, int take, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>().Skip(skip).Take(take).AsNoTracking();

            if (includes.Any())
            {
                includes.ToList().ForEach(i => query = query.Include(i));
            }

            var res =  await query.Where(predicate).ToListAsync();
            return res;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task EditAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

            if (includes.Any())
            {
                includes.ToList().ForEach(i => query = query.Include(i));
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
