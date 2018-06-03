using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.Models
{
    public class ActorMovie
    {
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
