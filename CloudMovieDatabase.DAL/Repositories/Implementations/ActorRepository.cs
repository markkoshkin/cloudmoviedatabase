using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.DAL.Repositories.Implementations
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(CloudMovieDatabaseContext dbContext)
       : base(dbContext)
        {

        }
    }
}
