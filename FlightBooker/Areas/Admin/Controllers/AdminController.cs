using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightBooker.DAL;
using FlightBooker.Buisness;

namespace FlightBooker.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private CustomerRepository customerRepo;
        public AdminController(CustomerRepository customerRepository)
		{
            customerRepo = customerRepository;
		}

        // GET: Admin/Admin
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") && UserHelper.count == 0)
            {
               UserHelper.count++;
            }
            return View();
        }

        public ActionResult ManageFlights()
		{
            return View();
		}

        public ActionResult ManageUsers()
        {
            var customers = customerRepo
                            .GetAllCustomers()
                            .Where(p => p.user_id != null)
                            .OrderBy(p => p.id)
                            .ToList();
            return View(customers);
        }

        public ActionResult ViewReports()
        {
            return View();
        } 
    }
}