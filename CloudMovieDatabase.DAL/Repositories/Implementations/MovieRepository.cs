using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMovieDatabase.DAL.Repositories.Implementations
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CloudMovieDatabaseContext dbContext)
     : base(dbContext)
        {

        }

        /// <summary>
        /// Method which will load all movie with actors
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Movie> GetByIdAsync(Guid id)
        {
            return await _dbContext.Movies.Include(e => e.Genre).Include(e => e.ActorMovie)
                    .ThenInclude(s => s.Actor)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
