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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _appDbContext;
        private IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            string? query = HttpContext.Request.Query["query"].ToString();
            List<CustomerDTO> customers = new List<CustomerDTO>();
            var raw_customers = _appDbContext.Customers.Include(c => c.MemberShipType);
            if (!string.IsNullOrWhiteSpace(query))
                raw_customers = raw_customers.Where(c => c.Name.Contains(query)).Include(c => c.MemberShipType);

            foreach (Customer c in raw_customers)
            {
                customers.Add(_mapper.Map<CustomerDTO>(c));
            }
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {

            var customer = _appDbContext.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == id);
            if(customer== null)
                return NotFound();

            return Ok(_mapper.Map<CustomerDTO>(customer));
        }

        [HttpPost]
        [Authorize(Roles=Roles.CanManageMovies)]
        public IActionResult CreateCustomer(CustomerDTO customer_dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Customer cus = _mapper.Map<Customer>(customer_dto);

            cus.MemberShipType = _appDbContext.MembershipTypes.SingleOrDefault(t => t.Id == cus.MemberShipTypeId);
            if(cus.MemberShipType == null)
                return BadRequest();

            try{
                _appDbContext.Customers.Add(cus);
                _appDbContext.SaveChanges(); 
            }
            catch (Exception ex) { return BadRequest(ex); }
            

            customer_dto.Id = cus.Id;
            Uri uri = new Uri(Request.GetDisplayUrl() + "/" + customer_dto.Id);
            return Created(uri, customer_dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult UpdateCustomer(int id, CustomerDTO customer_dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Customer? cus = _appDbContext.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == id);
            if(cus == null )
                return NotFound();

            _mapper.Map(customer_dto, cus);

            cus.MemberShipType = _appDbContext.MembershipTypes.SingleOrDefault(t => t.Id == cus.MemberShipTypeId);
            if (cus.MemberShipType == null)
                return BadRequest();

            try {
                _appDbContext.SaveChanges(); }
            catch (Exception ex) { return BadRequest(ex); }

            return Ok(customer_dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IActionResult DeleteCustomer(int id)
        {
            Customer? cus = _appDbContext.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == id);
            if (cus == null)
                return NotFound();

            _appDbContext.Customers.Remove(cus);
            _appDbContext.SaveChanges();

            return Ok();
        }
    }
}
