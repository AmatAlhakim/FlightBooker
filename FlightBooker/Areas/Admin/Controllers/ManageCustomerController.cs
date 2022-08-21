using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightBooker.Buisness;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FlightBooker.Models;
using FlightBooker.DAL;
using System.Data.Entity;
namespace FlightBooker.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageCustomerController : Controller
    {
        CustomerRepository customerRepo;

		public ManageCustomerController(CustomerRepository customerRepository)
		{
            customerRepo = customerRepository;
		}

        // GET: Admin/ManageCustomer
        public ActionResult Index()
        {
            var customers = customerRepo
                            .GetAllCustomers()
                            .Where(p => p.user_id != null)
                            .OrderBy(p => p.id)
                            .ToList();
            return View(customers);
        }

        public ActionResult Edit(int id)
        {
            FlightBooker.DAL.Customer customer = customerRepo.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(FlightBooker.DAL.Customer customer)
        {
			AspNetUserRepository userRepo = new AspNetUserRepository();
            ApplicationDbContext context = new ApplicationDbContext();
            if (customer.user_id != null && ModelState.IsValid)
			{
                var oldUser = context.Users.Find(customer.user_id);
                if (customer.first_name != null)
                    oldUser.UserName = customer.first_name;
                oldUser.Email = customer.email;
                oldUser.PasswordHash = customer.password;
                context.Entry(oldUser).State = EntityState.Modified;
                context.SaveChanges();
                customerRepo.Update(customer);
                TempData["SuccessMessage"] = "Customer Was Updated Successfully";
                return RedirectToAction("Index");
            }
			else
			{
                return View(customer);
			}
        }

        //[HttpPost]
        public ActionResult Delete(int id)
        {
            Customer customer = customerRepo.GetCustomerById(id);
            string userId = customer.user_id;
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser user = context.Users.Find(userId);
            context.Users.Remove(user);
            customerRepo.Delete(id);
            context.SaveChanges();
            //Display message to the user
            TempData["SuccessMessage"] = "Customer Was Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}