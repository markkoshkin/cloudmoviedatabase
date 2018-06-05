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
        private ActorMovieService _actorMovieService;

        public MovieController(MovieService movieService, ActorMovieService actorMovieService)
        {
            _movieService = movieService;
            _actorMovieService = actorMovieService;
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

