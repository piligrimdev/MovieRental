namespace WebApplication1.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        private string name;

        public string Name { get { return name; } set { name = value; } }

        public ushort MovieGenreId { get; set; }

        public GenreDTO? MovieGenre { get; set; }

        public byte Score { get; set; }

        public ushort Stock { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }
    }
}