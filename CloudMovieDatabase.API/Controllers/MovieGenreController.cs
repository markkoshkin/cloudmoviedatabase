using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudMovieDatabase.BLL.Services;
using CloudMovieDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudMovieDatabase.API.Controllers
{
    [Produces("application/json")]
    [Route("api/MovieGenres")]
    public class MovieGenreController : Controller
    {
        private MovieGenreService _movieGenreService;

        public MovieGenreController(MovieGenreService movieGenreService)
        {
            _movieGenreService = movieGenreService;
        }

        [HttpGet]
        public async Task<List<MovieGenre>> GetAll(int skip = 0, int take = 10)
        {
            return await _movieGenreService.GetAllAsync(skip, take);
        }
    }
}