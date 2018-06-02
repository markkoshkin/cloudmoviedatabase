using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMovieDatabase.Models
{
    /// <summary>
    /// Movie genre is implemented as class instead of enum for more flexibility
    /// For instance admin can create a new genre type via UI. Doesn't need deploy as in enum case
    /// Also better that just string in this case we can avoid fake genre in a system, more control from our side
    /// </summary>
    public class MovieGenre
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
