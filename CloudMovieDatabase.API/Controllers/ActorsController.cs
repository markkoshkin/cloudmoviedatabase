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
    [Route("api/[controller]")]
    public class ActorsController : Controller
    {
        private ActorService _actorService;

        public ActorsController(ActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet("GetAll")]//api/actors/getall?skip=2&take=15&isAttachMovies=true
        public async Task<List<Actor>> GetAll(int skip = 0, int take = 10, bool isAttachMovies = false)
        {
            return await _actorService.GetAllAsync(skip, take, isAttachMovies);
        }

        [HttpGet]
        [Route("GetActorById/{id:guid}/{isAttachMovies:bool?}")]///api/actors/GetActorById/3a96a2a2-fb1b-4f6a-b840-7fb27a846e8c/false
        public async Task<ActorUi> GetActorById(Guid id, bool isAttachMovies = true)
        {
            var res = await _actorService.FindByIdAsync(id, isAttachMovies);
            return res;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _actorService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Actor actor)
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
        public async Task<IActionResult> Put(Actor actor)
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
    }
}