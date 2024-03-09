using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Rental
    {
        public int Id { get; set; }
        [Required]        
        public Customer Customer { get; set; }
        [Required]
        public Movie Movie { get; set; }
        public DateTime DateRented { get; set; }
        // не обязательно должен быть объявлен
        public DateTime? DateReturned { get; set; }
    }
}
