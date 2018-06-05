using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private IMovieGenreRepository _movieGenreRepository;

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IActorMovieRepository actorMovieRepository, IMovieGenreRepository movieGenreRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _actorMovieRepository = actorMovieRepository;
            _movieGenreRepository = movieGenreRepository;
        }

        public async Task<List<Movie>> GetAllAsync(int skip, int take)
        {
            return await _movieRepository.AllAsync(skip, take);
        }

        public async Task<List<Movie>> GetAllAsync(int skip, int take, DateTime year)
        {
            return await _movieRepository.AllAsync(skip, take, e => e.Year.Year == year.Year);
        }

        public async Task<MovieUi> FindByIdAsync(Guid id, bool isAttachStarringActros)
        {
            if (isAttachStarringActros)
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                if (movie == null)
                {
                    throw new ArgumentException("Entity not found");
                }
                return movie.ConvertToUIModel();
            }
            else
            {
                var movie = await _movieRepository.FindByAsync(e => e.Id == id);
                if (movie == null)
                {
                    throw new ArgumentException("Entity not found");
                }
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

            var existedGenre = await _movieGenreRepository.FindByAsync(e => e.Id == movieUi.GenreId);
            if (existedGenre == null)
            {
                throw new ArgumentException($"Genre with id : {movieUi.GenreId} not found");
            }

            existedEntity.GenreId = movieUi.GenreId;
            existedEntity.Title = movieUi.Title;
            existedEntity.Year = movieUi.Year;

            await _movieRepository.EditAsync(existedEntity);
        }

        public async Task CreateAsync(MovieCreate movieUi)
        {
            var actorIds = new List<Guid>();

            if (movieUi.GenreId == Guid.Empty)
            {
                throw new ArgumentException($"Genre can't be null");
            }

            if (movieUi.FirstActor == Guid.Empty)
            {
                throw new ArgumentException($"Movie should has at least one actor");
            }

            var existedActor = await _actorRepository.FindByAsync(e => e.Id == movieUi.FirstActor);
            if (existedActor == null)
            {
                throw new ArgumentException($"Can't create movie with not existed actor id : {movieUi.FirstActor}");
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
            var actorMovie = new ActorMovie()
            {
                ActorId = movieUi.FirstActor,
                MovieId = movie.Id,
                ActorMovieId = Guid.NewGuid()
            };

            await _actorMovieRepository.AddAsync(actorMovie);
        }
    }
}
