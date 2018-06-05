using CloudMovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMovieDatabase.DAL.Repositories.Abstractions
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<Movie> GetByIdAsync(Guid id);
    }
}
