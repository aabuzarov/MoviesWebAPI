using MoviesWebAPI.Models;
using MoviesWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MoviesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        //private readonly MovieRepository _movieRepository;

        private readonly string _movieFolder = Path.Combine(Directory.GetCurrentDirectory(), "Media");

        public MovieController()
        {
            if (!Directory.Exists(_movieFolder))
            {
                Directory.CreateDirectory(_movieFolder);
            }

            //_movieRepository = new MovieRepository();
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            var videos = Directory.GetFiles(_movieFolder)
                                  .Select(Path.GetFileName)
                                  .Select(fileName => new Movie { FileName = fileName })
                                  .ToList();
            return Ok(videos);
        }

        [HttpGet("{fileName}")]
        public IActionResult GetMovie(string fileName)
        {
            var filePath = Path.Combine(_movieFolder, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, "video/mp4");
        }

        //[HttpPost]
        //public IActionResult PostFile(IFormFile file, FileUploadRequest model)


        //[HttpPost("upload")]
        [HttpPost]
        [RequestSizeLimit(209715200)] // 200MB
        public IActionResult UploadMovie([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".mp4")
            {
                return BadRequest("Only MP4 files are allowed.");
            }

            var filePath = Path.Combine(_movieFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok(new { file.FileName });
        }
    }
}

//[HttpGet]
//public ActionResult<IEnumerable<Movie>> GetMovies()
//{
//    return Ok(_movieRepository.GetAllMovies());
//}

//[HttpGet("{id}")]
//        public ActionResult<Movie> GetMovie(int id)
//        {
//            var movie = _movieRepository.GetMovieById(id);
//            if (movie == null)
//            {
//                return NotFound();
//            }
//            return Ok(movie);
//        }
//    }
//}