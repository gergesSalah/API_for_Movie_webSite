using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTO;
using WebApplication4.Services;

namespace WebApplication4.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        private new List<String> _alloweExtenstions = new List<string> { ".jpg", ".png" };
        private long _MaxAllowedPosterSize = 1048576;

        private readonly IMoviesService _moviesService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesService moviesService, IGenreService genreService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var movies = await _moviesService.GetAllMoviesAsync();

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto movieDto)
        {
            if (movieDto.Poster == null)
                return BadRequest("the poster is required");
            if (!_alloweExtenstions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                return BadRequest("only .png and .jpg images are allowed!");
            
            if (movieDto.Poster.Length > _MaxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isVaildGenre = await _genreService.IsVaildGenre(movieDto.GenreId) ;

            if (!isVaildGenre)
                return BadRequest("Invalid genere ID!");


            using var dataStream = new MemoryStream();

            await movieDto.Poster.CopyToAsync(dataStream);


            var movie = _mapper.Map<Movie>(movieDto);
            movie.Poster = dataStream.ToArray();
            _moviesService.Add(movie);

            var movieDet = _mapper.Map<MovieDetailsDto>(movieDto);
            movieDet.Poster = dataStream.ToArray();
            var genre = await _genreService.GetById(movieDet.GenreId);
            movieDet.GenreName = genre.Name;
            return Ok(movieDet);
            
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsyncById(int Id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(Id);

            if (movie == null)
                return NotFound();

            var moviedto = new MovieDetailsDto
            {
                Id = movie.ID,
                Title = movie.Title,
                Year = movie.Year,
                Rate = movie.Rate,
                GenreId = movie.GenreId,
                Storeline = movie.Storeline,
                GenreName = movie.Genre.Name,
                Poster = movie.Poster,

            };
            return Ok(moviedto);
        }

        [HttpGet("GetGenreMoviesByIdAsync")]
        public async Task<IActionResult> GetGenreMoviesByIdAsync(byte Genreid)
        {

            if (!await _genreService.IsVaildGenre(Genreid))
                return NotFound($"not found genre with id {Genreid}");

            var movies = await _moviesService.GetAllMoviesAsync(Genreid);
            

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
            //var movies = _context.Movies.Where(m => m.Genre.Id == Genreid).Include(M => M.Genre).ToListAsync();

            //if (movies == null)
            //    return NotFound();

            //return Ok(movies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id,[FromForm]MovieDto movieDto)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);

            if (movie == null)
                return NotFound($"not found movie with id {id}");

            var isVaildGenre = await _genreService.IsVaildGenre(movieDto.GenreId);

            if (!isVaildGenre)
                return BadRequest("Invalid genere ID!");

            if(movieDto.Poster!= null)
            {
                using var dataStream = new MemoryStream();

                await movieDto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }



            movie.Title = movieDto.Title;
            movie.Rate = movieDto.Rate;
            movie.GenreId = movieDto.GenreId;
            movie.Storeline = movieDto.Storeline;
            movie.Year = movieDto.Year;

            _moviesService.Update(movie);
            return Ok(movie);

            
            

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var moive = await _moviesService.GetMovieByIdAsync(id);

            if (moive == null)
                return NotFound($"No movie with id {id}");

            _moviesService.Delete(moive);
            return Ok(moive);

        }


    }
}
