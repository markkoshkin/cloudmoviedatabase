using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.DAL.Repositories.Implementations
{
    public class MovieGenreRepository : GenericRepository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(CloudMovieDatabaseContext dbContext)
      : base(dbContext)
        {

        }
    }
}
