using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;

namespace CloudMovieDatabase.DAL.Repositories.Implementations
{
    public class ActorMovieRepository : GenericRepository<ActorMovie>, IActorMovieRepository
    {
        public ActorMovieRepository(CloudMovieDatabaseContext dbContext)
      : base(dbContext)
        {

        }
    }
}
