using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudMovieDatabase.BLL.Services;
using CloudMovieDatabase.Models;
using CloudMovieDatabase.Models.Models.UiModels;
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

        [HttpGet("GetAll")] //api/movies/getall?skip=2&take=15
        public async Task<List<Movie>> GetAll(int skip = 0, int take = 10)
        {
            return await _movieService.GetAllAsync(skip, take);
        }

        [HttpGet]
        [Route("GetById/{id:guid}/{isAttachStarringActros:bool?}")]///api/movies/GetById/88b24550-a77d-47e4-ad12-1c36b5760477/false
        public async Task<MovieUi> GetActorById(Guid id, bool isAttachStarringActros = true)
        {
            return await _movieService.FindByIdAsync(id, isAttachStarringActros);
        }

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
