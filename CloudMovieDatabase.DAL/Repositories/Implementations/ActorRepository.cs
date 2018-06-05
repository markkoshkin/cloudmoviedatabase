using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMovieDatabase.DAL.Repositories.Implementations
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(CloudMovieDatabaseContext dbContext)
       : base(dbContext)
        {

        }

        /// <summary>
        /// Method which will load all actor and his filmography by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Actor> GetByIdAsync(Guid id)
        {
            return await _dbContext.Actors.Include(e => e.ActorMovie)
                    .ThenInclude(s => s.Movie)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
