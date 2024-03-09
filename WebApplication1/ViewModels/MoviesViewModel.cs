using WebApplication1.Models;
namespace WebApplication1.ViewModels
{
    public class MoviesViewModel
    {
        private List<Movie> movies;

        public List<Movie> Movies { get {  return movies; }  set { movies = value; } }

        public MoviesViewModel() { movies = new List<Movie>(); }

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }
    }
}
