using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightBooker.DAL;
using FlightBooker.Buisness;

namespace FlightBooker.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FlightController : Controller
    {
        FlightRepository flightRepo;

		public FlightController(FlightRepository flightRepository)
		{
            flightRepo = flightRepository;
        }

        // GET: Admin/Flight
        public ActionResult Index()
        {
            var flights = flightRepo
                          .GetAllFlights()
                          .OrderBy(p => p.flight_date)
                          .ToList();
            return View(flights);
        }

        //Get Request Method: Show the create form only
        [Authorize]
        public ActionResult Create()
		{
            return View();
		}

        [Authorize]
        [HttpPost]
        public ActionResult Create(Flight flight)
        {
            flightRepo.Add(flight);
            //Display message to the user
            TempData["SuccessMessage"] = "Flight Was Added Successfully";
            return RedirectToAction("Index", "Flight", new { area = "Admin" });
           // return Redirect(Url.Content("Admin/Conroller"));
        }

        public ActionResult Edit(int id)
		{
            Flight flight = flightRepo.GetFlightById(id);
            return View(flight);
		}

        [HttpPost]
        public ActionResult Edit(Flight flight)
		{
            flightRepo.Update(flight);
            //Display message to the user
            TempData["SuccessMessage"] = "Flight Was Updated Successfully";
            // return RedirectToAction("Index", "Admin", new { area = "Admin" });
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public ActionResult Delete(int id)
		{
            flightRepo.Delete(id);
            //Display message to the user
            TempData["SuccessMessage"] = "Flight Was Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}