using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(CloudMovieDatabaseContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
