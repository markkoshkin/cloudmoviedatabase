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
    [Route("api/Movies")]
    public class MovieController : Controller
    {
        private MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public async Task<List<Movie>> GetAll(int skip = 0, int take = 10, bool isAttachMovies = false)
        {
            return await _movieService.GetAll(skip, take, isAttachMovies);
        }

        //[HttpGet]
        //public async Task<Movie> GetActorById(Guid id, bool isAttachMovies = true)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Movie actor)
        {
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Movie actor)
        {
            return Ok();
        }
    }
}
