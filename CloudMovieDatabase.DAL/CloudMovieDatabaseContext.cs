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
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>()
                .HasKey(t => new { t.ActorId, t.MovieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(sc => sc.Actor)
                .WithMany(s => s.ActorMovie)
                .HasForeignKey(sc => sc.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(sc => sc.Movie)
                .WithMany(c => c.ActorMovie)
                .HasForeignKey(sc => sc.MovieId);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        //}

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<ActorMovie> ActorMovie { get; set; }
    }
}
