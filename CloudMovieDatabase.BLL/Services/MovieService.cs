﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloudMovieDatabase.Models;

namespace CloudMovieDatabase.BLL.Services
{
    public class MovieService
    {
        public Task<List<Movie>> GetAll(int skip, int take, bool isAttachMovies)
        {
            throw new NotImplementedException();
        }
    }
}
