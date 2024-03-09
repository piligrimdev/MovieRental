using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class MovieEditViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Movie? Movie { get; set; }
    }
}
