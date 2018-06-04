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
                var actors = new Actor[]
                {
                new Actor(){FirstName = "Chris", LastName = "Evans", Birthday=DateTime.Parse("1981-06-13"), Id =Guid.NewGuid()},
                new Actor(){ FirstName ="Robert", LastName ="Downey",Birthday=DateTime.Parse("1965-04-4"), Id =Guid.NewGuid()}
                };

                foreach (var actor in actors)
                {
                    context.Actors.Add(actor);
                }

                context.SaveChanges();
            }


        }
    }
}
