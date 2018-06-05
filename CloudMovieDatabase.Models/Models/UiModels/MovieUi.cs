using CloudMovieDatabase.Models.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CloudMovieDatabase.Models.Models.UiModels
{
    public class MovieUi
    {
        public Guid Id { get; set; }

        [StringLength(250, ErrorMessage = "Length should be more than 1 and less than 251 symbol", MinimumLength = 1)]
        public string Title { get; set; }

        [NotFutureYear(ErrorMessage = "Film can't be released in future")]
        public DateTime Year { get; set; }

        public Guid GenreId { get; set; }

        public MovieGenre Genre { get; set; }

        public List<Actor> StarringActros { get; set; }
    }
}
