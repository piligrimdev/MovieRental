using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CustomersViewModel
    {
        private List<Customer> customers;

        public List<Customer> Customers { get { return customers; } set { customers = value; } }

        public CustomersViewModel() { customers = new List<Customer>(); }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }
    }
}
