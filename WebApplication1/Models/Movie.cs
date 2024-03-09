using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        private string name;

        [Required]
        [MaxLength(255)]
        public string Name { get { return name; } set {  name = value; } }

        public Genre? MovieGenre { get; set; }

        //Также, как и в Customers
        [Required]
        [Display(Name = "Genre")]
        public int MovieGenreId { get; set; }

        [Required]
        [Range(0, 10)]
        public byte Score { get; set; }

        [Required]
        [Range(0, 100)]
        public int Stock { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [NotContainCussing]
        public string Description { get; set; }
    }
}
