using MoviesWebUI.Models;

namespace MoviesWebUI.Services
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task UploadMovieAsync(IFormFile file);
    }
}