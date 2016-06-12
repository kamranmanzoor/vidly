using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = GetCustomers();

            return View(customers);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer() {Name = "Kamran", Id = 1},
                new Customer() {Name = "Manzoor", Id = 2}
            };


        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => id == c.Id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}