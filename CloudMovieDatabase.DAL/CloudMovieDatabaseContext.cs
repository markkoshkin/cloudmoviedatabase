using CloudMovieDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.DAL
{
    public class CloudMovieDatabaseContext : DbContext
    {
        public CloudMovieDatabaseContext(DbContextOptions<CloudMovieDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
    }
}
