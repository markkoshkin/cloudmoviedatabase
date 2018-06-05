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
        private IActorMovieRepository _actorMovieRepository;

        public ActorService(IActorRepository actorRepository, IMovieRepository movieRepository, IActorMovieRepository actorMovieRepository)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _actorMovieRepository = actorMovieRepository;
        }

        public async Task<List<Actor>> GetAllAsync(int skip, int take)
        {
            return await _actorRepository.AllAsync(skip, take);
        }

        public async Task<ActorUi> FindByIdAsync(Guid id, bool isAttachMovies)
        {
            if (isAttachMovies)
            {
                var actor = await _actorRepository.GetByIdAsync(id);
                if (actor == null)
                {
                    throw new ArgumentException("Entity not found");
                }
                return actor.ConvertToUIModel();
            }
            else
            {
                var actor = await _actorRepository.FindByAsync(e => e.Id == id);
                if (actor == null)
                {
                    throw new ArgumentException("Entity not found");
                }
                return actor.ConvertToUIModel();
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existedEntity = await _actorRepository.GetByIdAsync(id);

            if (existedEntity == null)
            {
                throw new ArgumentException($"Actor with id : {id} not found");
            }

            // Due to film can't be without any actors we should check that all related film will have at least one actor after we remove this one
            foreach (var actorMovie in existedEntity.ActorMovie)
            {
                var movie = await _movieRepository.GetByIdAsync(actorMovie.MovieId);
                if (movie.ActorMovie.Count <= 1)
                {
                    throw new InvalidOperationException("Can't remove actor due to it is last film actor");
                }
            }

            //remove actorMovie
            foreach (var actorMovie in existedEntity.ActorMovie)
            {
                var actorMovieDb = await _actorMovieRepository.FindByAsync(e => e.ActorMovieId == actorMovie.ActorMovieId);
                await _actorMovieRepository.DeleteAsync(actorMovieDb);
            }

            //fetch again to avoid concurrent issues
            existedEntity = await _actorRepository.GetByIdAsync(id);
            if (existedEntity != null)
            {
                await _actorRepository.DeleteAsync(existedEntity);
            }
        }

        public async Task UpdateAsync(ActorUi actorUi)
        {
            var existedEntity = await _actorRepository.FindByAsync(e => e.Id == actorUi.Id);
            if (existedEntity == null)
            {
                throw new ArgumentException($"Actor with id : {actorUi.Id} not found");
            }

            existedEntity.LastName = actorUi.LastName;
            existedEntity.FirstName = actorUi.FirstName;
            existedEntity.Birthday = actorUi.Birthday;

            await _actorRepository.EditAsync(existedEntity);
        }

        public async Task CreateAsync(ActorUi actorUi)
        {
            var actor = new Actor()
            {
                Id = Guid.NewGuid(),
                Birthday = actorUi.Birthday,
                FirstName = actorUi.FirstName,
                LastName = actorUi.LastName
            };

            await _actorRepository.AddAsync(actor);
        }
    }
}
