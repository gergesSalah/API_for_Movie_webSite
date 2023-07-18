using System.Diagnostics.CodeAnalysis;

namespace WebApplication4.DTO
{
    public class MovieDto
    {
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public int Year { get; set; }
        [NotNull]
        public double Rate { get; set; }
        [NotNull]
        public string Storeline { get; set; }
        [NotNull]
        public IFormFile? Poster { get; set; }
        [NotNull]
        public byte GenreId { get; set; }
    }
}
