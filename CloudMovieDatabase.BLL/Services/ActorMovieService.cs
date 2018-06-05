using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMovieDatabase.BLL.Services
{
    public class ActorMovieService
    {
        IActorMovieRepository _actorMovieRepository;
        IActorRepository _actorRepository;
        IMovieRepository _movieRepository;

        public ActorMovieService(IActorMovieRepository actorMovieRepository, IActorRepository actorRepository, IMovieRepository movieRepository)
        {
            _actorMovieRepository = actorMovieRepository;
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
        }


        public async Task LinkActorAndMovie(Guid actorId, Guid movieId)
        {
            var actor = await _actorRepository.FindByAsync(e => e.Id == actorId);
            if (actor == null)
            {
                throw new ArgumentException($"Actor with id: {actorId} doesn't exist");
            }

            var movie = await _movieRepository.FindByAsync(e => e.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException($"Movie with id: {movieId} doesn't exist");
            }

            var actorMovie = await _actorMovieRepository.FindByAsync(e => e.ActorId == actorId && e.MovieId == movieId);
            if (actorMovie != null)
            {
                throw new ArgumentException($"Actor {actorId} and Movie {movieId} already linked");
            }

            actorMovie = new ActorMovie()
            {
                MovieId = movieId,
                ActorId = actorId,
                ActorMovieId = Guid.NewGuid()
            };

            await _actorMovieRepository.AddAsync(actorMovie);
        }
    }
}
