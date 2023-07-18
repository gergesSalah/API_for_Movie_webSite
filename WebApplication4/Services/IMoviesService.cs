namespace WebApplication4.Services
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetAllMoviesAsync(byte GenreId = 0);
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);

    }
}
