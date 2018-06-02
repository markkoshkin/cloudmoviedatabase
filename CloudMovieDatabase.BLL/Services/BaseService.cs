using CloudMovieDatabase.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.BLL.Services
{

    public class BaseService
    {
        private static CloudMovieDatabaseContext _context;

        protected CloudMovieDatabaseContext GetContext()
        {
            _context = _context ?? new CloudMovieDatabaseContext();

            return _context;
        }

    }
}
