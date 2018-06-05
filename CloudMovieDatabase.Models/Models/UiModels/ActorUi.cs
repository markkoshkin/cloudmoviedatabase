using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CloudMovieDatabase.Models.Models.UiModels
{
   public class ActorUi
    {
        public Guid Id { get; set; }

        [StringLength(60, ErrorMessage = "Length should be more than 1 and less than 61 symbol", MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(60, ErrorMessage = "Length should be more than 1 and less than 61 symbol", MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }


        public List<Movie> Filmography { get; set; }
    }
}
