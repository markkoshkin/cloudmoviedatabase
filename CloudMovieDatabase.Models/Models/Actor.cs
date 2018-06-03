using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CloudMovieDatabase.Models
{
    public class Actor
    {
        public Actor()
        {
            ActorMovie = new List<ActorMovie>();
        }

        public Guid Id { get; set; }

        [StringLength(60, ErrorMessage = "Length should be more than 1 and less than 61 symbol", MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(60, ErrorMessage = "Length should be more than 1 and less than 61 symbol", MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public List<ActorMovie> ActorMovie { get; set; }

        [NotMapped]
        public  List<Movie> Filmography { get; set; }
    }
}
