using MoviesWebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MoviesWebUI.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7185/api/Movie"; // Adjust the URL as needed

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Movie>>("https://localhost:7185/api/Movie");
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Movie>($"https://localhost:7185/api/Movie/{id}");
        }

        public async Task UploadMovieAsync(IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                using (var stream = file.OpenReadStream())
                {
                    content.Add(new StreamContent(stream), "file", file.FileName);
                    var response = await _httpClient.PostAsync(_apiUrl, content);
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}