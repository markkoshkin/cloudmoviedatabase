using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.DAL.Repositories.Implementations;
using CloudMovieDatabase.Models;

namespace CloudMovieDatabase.BLL.Services
{
    public class ActorService
    {
        private IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<List<Actor>> GetAll(int skip, int take, bool isAttachMovies)
        {
            return await _actorRepository.AllAsync();
        }
    }
}
