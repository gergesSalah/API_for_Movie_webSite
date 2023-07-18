using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace WebApplication4.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public int Year { get; set; }
        [NotNull]
        public double Rate { get; set; }
        [NotNull]
        public string Storeline { get; set; }
        [NotNull]
        public byte[] Poster { get; set; }
        [NotNull]
        public byte GenreId { get; set; }
        [NotNull]
        public Genre Genre { get; set; }
    }
}
