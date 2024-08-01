using MoviesWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoviesWebAPI.Repositories
{
    public class MovieRepository
    {
        private readonly List<Movie> _movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Year = 2010 },
            new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", Year = 1972 },
            new Movie { Id = 3, Title = "The Dark Knight", Genre = "Action", Year = 2008 }
        };

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movies;
        }

        public Movie GetMovieById(int id)
        {
            return _movies.FirstOrDefault(m => m.Id == id);
        }
    }
}