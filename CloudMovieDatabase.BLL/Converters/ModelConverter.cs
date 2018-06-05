using CloudMovieDatabase.Models;
using CloudMovieDatabase.Models.Models.UiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudMovieDatabase.BLL.Converters
{
    /// <summary>
    /// Due to we have simple models we are not going to use mappers
    /// </summary>
    public static class ModelConverter
    {
        public static ActorUi ConvertToUIModel(this Actor actor)
        {
            return new ActorUi()
            {
                Birthday = actor.Birthday,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Id = actor.Id,
                Filmography = actor.ActorMovie.Select(e => e.Movie).ToList()
            };
        }

        public static MovieUi ConvertToUIModel(this Movie movie)
        {
            return new MovieUi()
            {
                GenreId = movie.GenreId,
                Genre = movie.Genre,
                Title = movie.Title,
                Year = movie.Year,
                Id = movie.Id,
                StarringActros = movie.ActorMovie.Select(e => e.Actor).ToList()
            };
        }
    }
}
