using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMovieDatabase.BLL.Converters;
using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using CloudMovieDatabase.Models.Models.UiModels;

namespace CloudMovieDatabase.BLL.Services
{
    public class MovieService
    {
        private IMovieRepository _movieRepository;
        private IActorRepository _actorRepository;
        private IActorMovieRepository _actorMovieRepository;

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IActorMovieRepository actorMovieRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public async Task<List<Movie>> GetAllAsync(int skip, int take)
        {

            return await _movieRepository.AllAsync(skip, take);

        }

        public async Task<MovieUi> FindByIdAsync(Guid id, bool isAttachStarringActros)
        {
            if (isAttachStarringActros)
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                return movie.ConvertToUIModel();
            }
            else
            {
                var movie = await _movieRepository.FindByAsync(e => e.Id == id);
                return movie.ConvertToUIModel();
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existedEntity = await _movieRepository.GetByIdAsync(id);

            if (existedEntity == null)
            {
                throw new ArgumentException($"Movie with id : {id} not found");
            }

            //we need to remove film from actors 
            foreach (var actorMovie in existedEntity.ActorMovie)
            {
                await _actorMovieRepository.DeleteAsync(actorMovie);
            }

            await _movieRepository.DeleteAsync(existedEntity);
        }

        public async Task UpdateAsync(MovieUi movieUi)
        {
            var existedEntity = await _movieRepository.FindByAsync(e => e.Id == movieUi.Id);
            if (existedEntity == null)
            {
                throw new ArgumentException($"Movie with id : {movieUi.Id} not found");
            }

            if (movieUi.GenreId == Guid.Empty)
            {
                throw new ArgumentException($"Genre can't be null");
            }

            existedEntity.GenreId = movieUi.GenreId;
            existedEntity.Title = movieUi.Title;
            existedEntity.Year = movieUi.Year;

            await _movieRepository.EditAsync(existedEntity);
        }

        public async Task CreateAsync(MovieUi movieUi)
        {
            var actorIds = new List<Guid>();

            if (movieUi.GenreId == Guid.Empty)
            {
                throw new ArgumentException($"Genre can't be null");
            }

            if (!movieUi.StarringActros.Any())
            {
                throw new ArgumentException($"Movie should has at least one actor");
            }

            foreach (var actor in movieUi.StarringActros)
            {
                var existedActor = await _actorRepository.FindByAsync(e => e.Id == actor.Id);
                if (existedActor == null)
                {
                    throw new ArgumentException($"Can't create movie with not existed actor id : {actor.Id}");
                }
            }

            var movie = new Movie()
            {
                Genre = movieUi.Genre,
                GenreId = movieUi.GenreId,
                Id = Guid.NewGuid(),
                Title = movieUi.Title,
                Year = movieUi.Year,
            };

            await _movieRepository.AddAsync(movie);


            //Link movie to actors 
            foreach (var actor in movieUi.StarringActros)
            {
                var actorMovie = new ActorMovie()
                {
                    ActorId = actor.Id,
                    MovieId = movie.Id,
                    ActorMovieId = Guid.NewGuid()
                };

                await _actorMovieRepository.AddAsync(actorMovie);
            }
        }
    }
}
