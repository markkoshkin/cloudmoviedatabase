using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMovieDatabase.DAL.Repositories.Abstractions;
using CloudMovieDatabase.Models;

namespace CloudMovieDatabase.BLL.Services
{
    public class MovieGenreService
    {
        private IMovieGenreRepository _movieGenreRepository;
        public MovieGenreService(IMovieGenreRepository movieGenreRepository)
        {
            _movieGenreRepository = movieGenreRepository;
        }

        public async Task<List<MovieGenre>> GetAllAsync(int skip, int take)
        {
            return await _movieGenreRepository.AllAsync(skip, take);
        }

        public async Task Add(MovieGenre movieGenre)
        {
            movieGenre.Id = Guid.NewGuid();
            await _movieGenreRepository.AddAsync(movieGenre);
        }
    }
}
