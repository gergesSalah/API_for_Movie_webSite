namespace WebApplication4.Services
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Add (Genre genre);
        Genre Update (Genre genre);
        Genre Delete(Genre genre);
        Task<bool> IsVaildGenre(byte id);


    }
}
