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
    [Route("api/Actors")]
    public class ActorsController : Controller
    {
        private ActorService _actorService;

        public ActorsController(ActorService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Action to return all actors 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="isAttachMovies"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Actor>> GetAll(int skip = 0, int take = 10, bool isAttachMovies = false)
        {
            
            return await _actorService.GetAll(skip, take, isAttachMovies);
        }


        [HttpGet]
        public async Task<Actor> GetActorById(Guid id, bool isAttachMovies = true)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Actor actor)
        {
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Actor actor)
        {
            return Ok();
        }
    }
}