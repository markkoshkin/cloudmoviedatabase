using CloudMovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMovieDatabase.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(CloudMovieDatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Actors.Any())
            {
                var actors = new List<Actor>
                {
                     new Actor(){ FirstName = "Chris", LastName = "Evans",  Birthday=DateTime.Parse("1981-06-13"), Id = Guid.Parse("3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c")},
                     new Actor(){ FirstName ="Robert", LastName ="Downey", Birthday=DateTime.Parse("1965-04-4"), Id = Guid.Parse("c40145bf-8c34-42b9-8e78-92b57cf7d55f")}
                };

                foreach (var actor in actors)
                {
                    context.Actors.Add(actor);
                }

                context.SaveChanges();
            }

            if (!context.MovieGenres.Any())
            {
                var genres = new List<MovieGenre>()
                {
                    new MovieGenre(){Id = Guid.Parse("16f2eadb-61f7-41b0-9ec1-4feca4a0aad9"), Name = "Adventure"},
                    new MovieGenre(){Id = Guid.Parse("85148c38-a86e-4268-b1a5-6ea70fb73426"), Name = "Comedy"},
                    new MovieGenre(){Id = Guid.Parse("dbf42767-49d4-40f5-9fb1-e502e545f019"), Name = "Western"}
                };

                foreach (var genre in genres)
                {
                    context.MovieGenres.Add(genre);
                }

                context.SaveChanges();
            }


            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie(){Id = Guid.Parse("88b24550-a77d-47e4-ad12-1c36b5760477"), Title = "Avengers", GenreId = Guid.Parse("16f2eadb-61f7-41b0-9ec1-4feca4a0aad9"), Year = DateTime.Parse("2012-06-4") },
                    new Movie(){Id = Guid.Parse("1c2d6157-c48c-432a-8b7f-331a74df246e"), Title = "Avengers: Age of Ultron", GenreId = Guid.Parse("16f2eadb-61f7-41b0-9ec1-4feca4a0aad9"), Year = DateTime.Parse("2015-06-4") },
                    new Movie(){Id = Guid.Parse("cca94609-68bd-4f54-95a1-5072adf26316"), Title = "Avengers: Infinity war", GenreId = Guid.Parse("16f2eadb-61f7-41b0-9ec1-4feca4a0aad9"), Year = DateTime.Parse("2018-06-4") },
                };

                foreach(var movie in movies)
                {
                    context.Movies.Add(movie);
                }

                context.SaveChanges();
            }

            if (!context.ActorMovie.Any())
            {
                var actorMovies = new List<ActorMovie>()
                {
                    new ActorMovie(){ActorMovieId = Guid.NewGuid(), ActorId = Guid.Parse("3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c"), MovieId = Guid.Parse("88b24550-a77d-47e4-ad12-1c36b5760477")},
                    new ActorMovie(){ActorMovieId = Guid.NewGuid(), ActorId = Guid.Parse("3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c"), MovieId = Guid.Parse("1c2d6157-c48c-432a-8b7f-331a74df246e")},
                    new ActorMovie(){ActorMovieId = Guid.NewGuid(), ActorId = Guid.Parse("3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c"), MovieId = Guid.Parse("cca94609-68bd-4f54-95a1-5072adf26316")},

                    new ActorMovie(){ActorMovieId = Guid.NewGuid(), ActorId = Guid.Parse("c40145bf-8c34-42b9-8e78-92b57cf7d55f"), MovieId = Guid.Parse("88b24550-a77d-47e4-ad12-1c36b5760477")},
                    new ActorMovie(){ActorMovieId = Guid.NewGuid(), ActorId = Guid.Parse("c40145bf-8c34-42b9-8e78-92b57cf7d55f"), MovieId = Guid.Parse("1c2d6157-c48c-432a-8b7f-331a74df246e")},
                };

                foreach (var actorMovie in actorMovies)
                {
                    context.ActorMovie.Add(actorMovie);
                }

                context.SaveChanges();
            }
        }
    }
}
