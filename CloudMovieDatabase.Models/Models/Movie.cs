using CloudMovieDatabase.Models.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudMovieDatabase.Models
{
    public class Movie
    {
        public Movie()
        {
            ActorMovie = new List<ActorMovie>();
        }

        public Guid Id { get; set; }

        [StringLength(250, ErrorMessage = "Length should be more than 1 and less than 251 symbol", MinimumLength = 1)]
        public string Title { get; set; }

        [NotFutureYear(ErrorMessage = "Film can't be released in future")]
        public DateTime Year { get; set; }

        public virtual MovieGenre Genre { get; set; }

        public List<ActorMovie> ActorMovie { get; set; }

        [NotMapped]
        public List<Actor> StarringActros { get; set; }

    }
}
