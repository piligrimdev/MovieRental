using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private MoviesViewModel _viewModel;
        private ApplicationDbContext _appDbContext;

        public MoviesController(ApplicationDbContext context) { 
            _viewModel = new MoviesViewModel();
            _appDbContext = context;

            _viewModel.Movies = _appDbContext.Movies.Include(c => c.MovieGenre).ToList();
        }
        protected override void Dispose(bool disposing)
        {
            _appDbContext.Dispose();
        }

        [Authorize(Roles = Roles.CanManageMovies)]
        [Route("Movies/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            Movie? movie = _appDbContext.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                movie = new Movie();
            }
            var vm = new MovieEditViewModel
            {
                Movie = movie,
                Genres = _appDbContext.Genres.ToList()
            };
            return View(vm);
        }

        [Authorize(Roles = Roles.CanManageMovies)]
        [HttpPost]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var vm = new MovieEditViewModel
                {
                    Movie = movie,
                    Genres = _appDbContext.Genres.ToList()
                };
                return View("Edit", vm);
            }
            if (movie.Id == 0)
            {
                movie.MovieGenre = _appDbContext.Genres.Single(t => t.Id == movie.MovieGenreId);
                _appDbContext.Movies.Add(movie);
            }
            else
            {
                var curCustomer = _appDbContext.Movies.Single(c => c.Id == movie.Id);

                curCustomer.Name = movie.Name;
                curCustomer.ReleaseDate = movie.ReleaseDate;
                curCustomer.Score = movie.Score;
                curCustomer.Stock = movie.Stock;
                curCustomer.Description = movie.Description;
                curCustomer.MovieGenre = _appDbContext.Genres.Single(t => t.Id == movie.MovieGenreId);
                curCustomer.MovieGenreId = movie.MovieGenreId;
            }
            _appDbContext.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [Route("Movies/")]
        public IActionResult Index()
        {
            if (User.IsInRole(Roles.CanManageMovies))
                return View("EditList");
            return View("List");
        }

        [Route("Movies/Details/{id}")]
        public IActionResult Details(int id)
        {
            //Movie? mov  = _viewModel.Movies.Find(x => x.Id == id);
            Movie? mov = _appDbContext.Movies.SingleOrDefault(x => x.Id == id);
            if (mov == null)
                return NotFound();
            return View(mov);
        }
    }
}
