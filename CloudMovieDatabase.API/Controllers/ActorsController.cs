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
    [Route("api/[controller]")]
    public class ActorsController : Controller
    {
        private ActorService _actorService;
        private ActorMovieService _actorMovieService;
        private MovieService _movieService;

        public ActorsController(ActorService actorService, ActorMovieService actorMovieService, MovieService movieService)
        {
            _actorService = actorService;
            _actorMovieService = actorMovieService;
            _movieService = movieService;
        }

        [HttpGet("GetAll")]//api/actors/getall?skip=2&take=15
        public async Task<List<Actor>> GetAll(int skip = 0, int take = 10)
        {
            return await _actorService.GetAllAsync(skip, take);
        }

        [HttpGet]
        [Route("GetActorsByMovieId/{id:guid}")]
        public async Task<List<ActorUi>> GetActorsByMovieId(Guid id)
        {
            var movie = await _movieService.FindByIdAsync(id, true);
            return movie.StarringActros.Select(e => e.ConvertToUIModel()).ToList();
        }


        [HttpGet]
        [Route("GetActorById/{id:guid}/{isAttachMovies:bool?}")]///api/actors/GetActorById/3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c/false
        public async Task<ActorUi> GetActorById(Guid id, bool isAttachMovies = true)
        {
            var res = await _actorService.FindByIdAsync(id, isAttachMovies);
            return res;
        }

        [HttpDelete] //http://localhost:2964/api/actors?id=3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c
        public async Task<IActionResult> Delete(Guid id)
        {
            await _actorService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActorUi actor)
        {
            if (ModelState.IsValid)
            {
                await _actorService.CreateAsync(actor);
                return StatusCode(201);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ActorUi actor)
        {
            if (ModelState.IsValid)
            {
                await _actorService.UpdateAsync(actor);
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