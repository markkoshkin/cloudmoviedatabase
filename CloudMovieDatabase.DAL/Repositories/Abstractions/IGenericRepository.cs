using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CloudMovieDatabase.DAL.Repositories.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<List<T>> All(IOrderedQueryable<T> orderBy = null,
        //    Expression<Func<T, bool>> filter = null,
        //    params Expression<Func<T, object>>[] includes);

        //Task<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> All();
        Task Add(T entity);
        Task Delete(T entity);
        Task Edit(T entity);
    }
}
