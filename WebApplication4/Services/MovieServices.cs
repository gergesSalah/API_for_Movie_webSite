using WebApplication4.DTO;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public class MovieServices : IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<List<Movie>> GetAllMoviesAsync(byte GenreId = 0)
        {
            return await _context.Movies.OrderByDescending(m => m.Rate).
                Where(m=>m.GenreId == GenreId || GenreId == 0).
                Include(m => m.Genre)
                .ToListAsync();



            
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Genre).
                SingleOrDefaultAsync(m => m.ID == id);
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
