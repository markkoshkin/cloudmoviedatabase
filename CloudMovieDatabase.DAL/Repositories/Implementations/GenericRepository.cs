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
        private readonly CloudMovieDatabaseContext _dbContext;

        public GenericRepository(CloudMovieDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> AllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
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

        //public async List<List<T>> All(IOrderedQueryable<T> orderBy = null, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (includes.Any())
        //    {
        //        includes.ToList().ForEach(i => query = query.Include(i));
        //    }

        //    //TODO Fix bug
        //    //if (orderBy != null)
        //    //{
        //    //    query = orderBy(query);
        //    //}

        //    return query.ToList();
        //}

        //public T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

        //    if (includes.Any())
        //    {
        //        includes.ToList().ForEach(i => query = query.Include(i));
        //    }

        //    return query.FirstOrDefault(predicate);
        //}
    }
}
