using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudMovieDatabase.BLL.Converters;
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
        private ActorMovieService _actorMovieService;
        private ActorService _actorService;

        public MovieController(MovieService movieService, ActorMovieService actorMovieService, ActorService actorService)
        {
            _movieService = movieService;
            _actorMovieService = actorMovieService;
            _actorService = actorService;
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

        [HttpGet]
        [Route("GetMoviesByActorId/{id:guid}")]///api/movies/GetById/88b24550-a77d-47e4-ad12-1c36b5760477/false
        public async Task<List<MovieUi>> GetMoviesByActorId(Guid id)
        {
            var actor = await _actorService.FindByIdAsync(id, true);
            return actor.Filmography.Select(e => e.ConvertToUIModel()).ToList();
        }

        [HttpGet]
        [Route("GetMoviesByReleaseDateYear/{year:int}")]///api/movies/GetById/88b24550-a77d-47e4-ad12-1c36b5760477/false
        public async Task<List<MovieUi>> GetMoviesByReleaseDateYear(int year)
        {
            var time = new DateTime(year, 1, 1, 1, 1, 0);
            var movies = await _movieService.GetAllAsync(0, 10, time);
            return movies.Select(e => e.ConvertToUIModel()).ToList();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _movieService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(MovieUi movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(movie);
                return StatusCode(201);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(MovieUi movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.UpdateAsync(movie);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // [HttpPut("LinkActorAndMovie/{actorId:Guid}/{movieId:Guid}")]
        [HttpPost("LinkActorAndMovie")]
        public async Task<IActionResult> LinkActorAndMovie(Guid actorId, Guid movieId)
        {
            await _actorMovieService.LinkActorAndMovie(actorId, movieId);
            return Ok();
        }

        // [HttpPut("UnLinkActorAndMovie/{actorId:Guid}/{movieId:Guid}")]
        [HttpPost("UnLinkActorAndMovie")]
        public async Task<IActionResult> UnLinkActorAndMovie(Guid actorId, Guid movieId)
        {
            await _actorMovieService.UnLinkActorAndMovie(actorId, movieId);
            return Ok();
        }
    }
}

