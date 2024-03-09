using WebApplication1.DTOs;
using WebApplication1.Models;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.AccessControl;

namespace WebApplication1.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ApplicationDbContext _appDbContext;
        private IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAllMovies()
        {
            string? query = HttpContext.Request.Query["query"].ToString();
            List<MovieDTO> movies = new List<MovieDTO>();
            var raw_movies = _appDbContext.Movies.Include(m => m.MovieGenre);
            if (!string.IsNullOrWhiteSpace(query))
                raw_movies = raw_movies
                    .Where(m => m.Name.Contains(query)).Include(m => m.MovieGenre)
                    .Where(m => m.Stock > 0)
                    .Include(m => m.MovieGenre);

            foreach (Movie c in raw_movies)
            {
                MovieDTO dto = _mapper.Map<MovieDTO>(c);
                //dto.Genre = _mapper.Map<GenreDTO>(_appDbContext.Genres.Single(g => g.Id == dto.MovieGenreId));
                movies.Add(dto);
            }
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {

            var movie = _appDbContext.Movies
                .Include(m => m.MovieGenre)
                .SingleOrDefault(c => c.Id == id);
            MovieDTO dto = _mapper.Map<MovieDTO>(movie);
            //dto.Genre = _mapper.Map<GenreDTO>(_appDbContext.Genres.Single(g => g.Id == dto.MovieGenreId));
            if (movie == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult CreateMovie(MovieDTO movie_dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Movie mov = _mapper.Map<Movie>(movie_dto);

            mov.MovieGenre = _appDbContext.Genres.SingleOrDefault(t => t.Id == mov.MovieGenreId);
            if (mov.MovieGenre == null)
                return BadRequest();

            try
            {
                _appDbContext.Movies.Add(mov);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex) { return BadRequest(ex); }


            movie_dto.Id = mov.Id;
            Uri uri = new Uri(Request.GetDisplayUrl() + "/" + movie_dto.Id);
            return Created(uri, movie_dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult UpdateMovie(int id, MovieDTO movie_dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Movie? mov = _appDbContext.Movies.SingleOrDefault(c => c.Id == id);
            if (mov == null)
                return NotFound();

            _mapper.Map(movie_dto, mov);

            mov.MovieGenre = _appDbContext.Genres.SingleOrDefault(t => t.Id == mov.MovieGenreId);
            if (mov.MovieGenre == null)
                return BadRequest();

            try { 
                _appDbContext.SaveChanges(); 
            }

            catch (Exception ex) { return BadRequest(ex); }

            return Ok(mov);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult DeleteMovie(int id)
        {
            Movie? mov = _appDbContext.Movies.SingleOrDefault(c => c.Id == id);
            if (mov == null)
                return NotFound();

            _appDbContext.Movies.Remove(mov);
            _appDbContext.SaveChanges();

            return Ok();
        }
    }
}
