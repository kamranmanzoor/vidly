using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer() {Name = "Kamran", Id = 1},
                new Customer() {Name = "Manzoor", Id = 2}
            };

            TempData["customers"] = customers;

            ViewBag.Customers = customers;
            return View();
        }

        public ActionResult Details(int id)
        {
            string name = string.Empty;

            if (TempData["customers"] != null)
            {
                //var customer = TempData["customers"] as Customer;
                //var customer = (Object[])TempData["customers"];

            }

            return Content(name);
        }
    }
}