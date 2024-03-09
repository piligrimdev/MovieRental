using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private CustomersViewModel _viewModel;
        private ApplicationDbContext _appDbContext;

        public CustomersController(ApplicationDbContext context)
        {
            _viewModel = new CustomersViewModel();
            _appDbContext = context;

            // Include - System.Data.Entity;
            _viewModel.Customers = _appDbContext.Customers.Include(c => c.MemberShipType).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            _appDbContext.Dispose();
        }

        [Route("Customers/")]
        public IActionResult Index()
        {
            if(User.IsInRole(Roles.CanManageMovies))
                return View("EditList");
            return View("List");
        }

        [Authorize(Roles=Roles.CanManageMovies)]
        [Route("Customers/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            Customer? customer = _appDbContext.Customers.SingleOrDefault(x => x.Id == id);
            if(customer == null)
            {
                customer = new Customer();
            }
            var vm = new EditCustomerViewModel
            {
                Customer = customer,
                MemberShipTypes = _appDbContext.MembershipTypes.ToList()
            };
            return View(vm);
        }

        [Authorize(Roles = Roles.CanManageMovies)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid )
            {
                var vm = new EditCustomerViewModel
                {
                    Customer = customer,
                    MemberShipTypes = _appDbContext.MembershipTypes.ToList()
                };
                return View("Edit", vm);
            }
            if(customer.Id == 0)
            {
                customer.MemberShipType = _appDbContext.MembershipTypes.Single(t => t.Id == customer.MemberShipTypeId);
                _appDbContext.Customers.Add(customer);
            }
            else
            {
                var curCustomer = _appDbContext.Customers.Single(c => c.Id == customer.Id);
                curCustomer.Name  = customer.Name;
                curCustomer.BirthDay   = customer.BirthDay;
                curCustomer.MemberShipType = _appDbContext.MembershipTypes.Single(t => t.Id == customer.MemberShipTypeId);
            }
            _appDbContext.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [Route("Customers/Details/{id}")]
        public IActionResult Details(int id)
        {
            //Customer? cus = _viewModel.Customers.Find(x => x.Id == id);
            Customer? cus = _appDbContext.Customers.SingleOrDefault(x => x.Id == id);
            if (cus == null)
                return NotFound();
            return View(cus);
        }
    }
}
