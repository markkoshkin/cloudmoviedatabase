using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudMovieDatabase.BLL.Converters;
using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using CloudMovieDatabase.Models.Models.UiModels;

namespace CloudMovieDatabase.BLL.Services
{
    public class ActorService
    {
        private IActorRepository _actorRepository;
        private IMovieRepository _movieRepository;

        public ActorService(IActorRepository actorRepository, IMovieRepository movieRepository)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
        }

        public async Task<List<Actor>> GetAllAsync(int skip, int take, bool isAttachMovies)
        {
            return await _actorRepository.AllAsync(skip, take);
        }

        public async Task<ActorUi> FindByIdAsync(Guid id, bool isAttachMovies)
        {
            if (isAttachMovies)
            {
                var actor = await _actorRepository.GetByIdAsync(id);
                return actor.ConvertToUIModel();
            }
            else
            {
                var actor = await _actorRepository.FindByAsync(e => e.Id == id);
                return actor.ConvertToUIModel();
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existedEntity = await FindByIdAsync(id, true);

            if (existedEntity == null)
            {
                throw new ArgumentException($"Actor with id : {id} not found");
            }

            // Due to film can't be without any actors we should check that all related film will have at least one actor after we remove this one
            foreach (var film in existedEntity.Filmography)
            {
                var dbFilm = await _movieRepository.FindByAsync(e => e.Id == film.Id, e => e.StarringActros);
                if (dbFilm.StarringActros.FirstOrDefault(e => e.Id != id) == null)
                {
                    throw new ArgumentException($"Can't remove actor with id {id} due to some film will be without actors");
                }
            }

            //remove actor from films
            foreach (var film in existedEntity.Filmography)
            {
                var dbFilm = await _movieRepository.FindByAsync(e => e.Id == film.Id, e => e.StarringActros);
                dbFilm.StarringActros.RemoveAt(dbFilm.StarringActros.FindIndex(a => a.Id == id));

                await _movieRepository.EditAsync(dbFilm);
            }

            //  await _actorRepository.DeleteAsync(existedEntity);
        }

        public async Task UpdateAsync(Actor actor)
        {
            var existedEntity = await FindByIdAsync(actor.Id, false);
            if (existedEntity == null)
            {
                throw new ArgumentException($"Actor with id : {actor.Id} not found");
            }

            await _actorRepository.EditAsync(actor);
        }

        public async Task CreateAsync(Actor actor)
        {
            actor.Id = Guid.NewGuid();
            await _actorRepository.AddAsync(actor);
        }
    }
}
