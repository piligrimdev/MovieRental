using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http.Extensions;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private ApplicationDbContext _appDbContext;
        private IMapper _mapper;

        public RentalController(ApplicationDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRentals()
        {
            return Ok("Rentals data");
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            return Ok("Specific rental data");
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateRental(RentalDTO rental_dto)
        {
            Customer? cus = _appDbContext.Customers.SingleOrDefault(c => c.Id == rental_dto.CustomerId);
            if (cus == null)
                return BadRequest();
            var movies = _appDbContext.Movies
                .Where(m => rental_dto.MoviesIds.Contains(m.Id))
                .Where(m => m.Stock > 0);
            
            foreach (Movie movie in movies)
            {
                Rental r = new Rental()
                {
                    Customer = cus,
                    Movie =movie,
                    DateRented = DateTime.Now.Date
                };
                movie.Stock--;
                _appDbContext.Rentals.Add(r);
            }
            try
            {
                _appDbContext.SaveChanges();
            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
            return Ok("Created rentals");
        }


        //Типа как в магазинах удалять могут только старшие продавцы
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult UpdateRental(int id, RentalDTO movie_dto)
        {
            return Ok("Updated rental");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult DeleteMovie(int id)
        {
            return Ok("Deleted rental");
        }
    }
}
