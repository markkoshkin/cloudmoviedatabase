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

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository)
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
            var existedEntity = await FindByIdAsync(id, true);

            if (existedEntity == null)
            {
                throw new ArgumentException($"Movie with id : {id} not found");
            }

            //we need to remove film from actors 
            foreach (var actor in existedEntity.StarringActros)
            {
                var dbActor = await _actorRepository.FindByAsync(e => e.Id == actor.Id, e => e.Filmography);
                dbActor.Filmography.RemoveAt(dbActor.Filmography.FindIndex(a => a.Id == id));

                await _actorRepository.EditAsync(dbActor);
            }

          //  await _movieRepository.DeleteAsync(existedEntity);
        }

        public async Task UpdateAsync(Movie movie)
        {
            var existedEntity = await FindByIdAsync(movie.Id, false);
            if (existedEntity == null)
            {
                throw new ArgumentException($"Movie with id : {movie.Id} not found");
            }

            await _movieRepository.EditAsync(movie);
        }

        public async Task CreateAsync(Movie movie)
        {
            movie.Id = Guid.NewGuid();
            if (!movie.StarringActros.Any())
            {
                throw new ArgumentException($"Movie should has at least one actor");
            }

            await _movieRepository.AddAsync(movie);
        }
    }
}
