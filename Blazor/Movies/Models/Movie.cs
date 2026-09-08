namespace Movies.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateOnly RealeaseDate { get; set; }
        public string? Genre { get; set; }
        public string? URL { get; set; }
        public string Poster { get; set; }
    }
}
