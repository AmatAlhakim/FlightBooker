using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightBooker.Buisness;
using FlightBooker.DAL;

namespace FlightBooker.Controllers
{
    
    public class CustomerController : Controller
    {
        CustomerRepository customerRepo;
        ReservationHistoryRepository histroyRepo;
        FlightReservationRepository reservationRepo;
        FlightRepository flightRepo; 

        public CustomerController(CustomerRepository customerReporsitry, 
            ReservationHistoryRepository reservationHistory, 
            FlightReservationRepository flightReservation,
            FlightRepository flightRepository)
        {
            customerRepo = customerReporsitry;
            histroyRepo = reservationHistory;
            reservationRepo = flightReservation;
            flightRepo = flightRepository;
        }

        [Authorize]
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ReservationHistory()
        {
            // 1- Get Current User ID
            string userId = UserHelper.GetCurrentUserId();
            // 2- Get Customer With The Same UserID
            FlightBooker.DAL.Customer currentCustomer = customerRepo.GetCustomerByUserId(userId);
            // 3- Get all reservations done by this customer
            List<Reservation_History> histories = histroyRepo.GetAllReservationHistoryByUserId(currentCustomer.id);
            // 4- Save the reservations ids done by this user in a list
            List<int> reservationsIds = new List<int>();
            if (histories.Count > 0)
            {
                foreach (var item in histories)
                {
                    reservationsIds.Add((int)item.reservation_id);
                }
            }

            List<Flight_Reservation> flightReservations = reservationRepo.GetAllFlightReservations(); ;
            List<int> flightsIds = new List<int>();
            // 5- Get all flights ids booked by this user
            if (flightReservations.Count > 0)
            {
                foreach (var item in flightReservations)
                {
                    if (reservationsIds.Contains((int)item.reservation_id))
                    {
                        flightsIds.Add((int)item.flight_id);
                    }
                }
            }

            // 6- Get all flights booked by this user 
            List<Flight> flights = flightRepo.GetAllFlights();
            List<Flight> bookedFlights = new List<Flight>();

            if (flightsIds.Count > 0)
            {
                foreach (var item in flights)
                {
                    if (flightsIds.Contains((int)item.id))
					{
                        bookedFlights.Add(item);
                    }
                }
            }

            if (bookedFlights.Count > 0)
            {
                return View(bookedFlights);
            }
            else
                return Content("You Don't Booked Flights Yet");
        }
    }
}