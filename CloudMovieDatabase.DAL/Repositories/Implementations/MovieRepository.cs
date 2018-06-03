using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.DAL.Repositories.Implementations
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CloudMovieDatabaseContext dbContext)
     : base(dbContext)
        {

        }
    }
}
