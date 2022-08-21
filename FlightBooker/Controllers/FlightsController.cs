using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightBooker.DAL;
using FlightBooker.Buisness;
using System.ComponentModel.DataAnnotations;

namespace FlightBooker.Controllers
{
    [AllowAnonymous]
    public class FlightsController : Controller
    {
        private FlightRepository flightRepo;
        private PassengerRepository passengerRepo;
        ReservationRepository reservationRepo;
        FlightReservationRepository flightReservationRepo;
        PassengerReservationRepository passengerReservationRepo;
        ItineraryRepository itineraryRepo;
        CustomerRepository customerRepo;
        ReservationHistoryRepository historyRepo;

        public FlightsController(FlightRepository flightRepository,
            PassengerRepository passengerRepository, ReservationRepository reservationRepository,
            FlightReservationRepository flightReservationRepository, 
            PassengerReservationRepository passengerReservation, ItineraryRepository itineraryRepository,
            CustomerRepository customerRepository, ReservationHistoryRepository reservationHistoryRepository)
		{
            flightRepo = flightRepository;
            passengerRepo = passengerRepository;
            reservationRepo = reservationRepository;
            flightReservationRepo = flightReservationRepository;
            passengerReservationRepo = passengerReservation;
            itineraryRepo = itineraryRepository;
            customerRepo = customerRepository;
            historyRepo = reservationHistoryRepository;
        }

        // GET: Flights
        public ActionResult Index()
        {
            return View();
        }

        //Search Flights List Page
        public ActionResult SearchResult()
		{
            ViewBag.classType = Session["classType"].ToString();

            if (Session["Origin"] != null && Session["Destination"] != null)
			{
                string v = ",";
                int orgingIndex = (int) Session["Origin"].ToString().IndexOf(v);

                string origin = Session["Origin"].ToString().Substring(0, orgingIndex);
                int destinationIndex = (int)Session["Destination"].ToString().IndexOf(v);
                string destination = Session["Destination"].ToString().Substring(0, destinationIndex);

                List<Flight> flights = new List<Flight>();

                if (Session["Date"] != null)
				{
                    DateTime date = Convert.ToDateTime( Session["Date"].ToString());
                    flights = flightRepo.Filter(origin, destination, date);
                }
				else
				{
                    flights = flightRepo.Filter(origin, destination);                    
                }

                //Sort the list
                if (Session["SortBy"] != null)
                {
                    //List<Flight> sortedFlights = new List<Flight>();
                    string sortBy = Session["SortBy"].ToString();
                    string classType = Session["classType"].ToString();
                    List<Flight> sortedFlights = flightRepo.SortedList(sortBy,classType, flights);
                    return View(sortedFlights);
                }
                else
                {
                    return View(flights);
                }
            }            
            else return Content("No Flights Available");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(Search search)
        {
            Session["Origin"] = search.origin;
            Session["Destination"] = search.destination;
            Session["Date"] = search.date;
            Session["FlightType"] = search.flightType;
            Session["SortBy"] = search.shortBy_Id;
            Session["classType"] = search.classType;
            if (Session["classType"].ToString() != null)
                ViewBag.classType = Session["classType"];
            return RedirectToAction("SearchResult", "Flights");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(Flight flight)
        {
            ViewBag.FlightId = flight.id;
            Session["FlightId"] = flight.id;
			return RedirectToAction("Book");
		}

        //Book flight Details
        public ActionResult Book(int id)
		{
            ViewBag.FlightType = Session["FlightType"];
            //Should check if the flight type if its transit or direct
            Flight flight = flightRepo.GetFlightById(id);
            ViewBag.FlightId = id;
            Session["FlightId"] = id;
            return View(flight);
		}

        // Round-Trip Flights List Page
        public ActionResult ReturnFlight()
        {
            ViewBag.classType = Session["classType"].ToString();
            string v = ",";
            int orgingIndex = (int)Session["Origin"].ToString().IndexOf(v);
            string origin = Session["Origin"].ToString().Substring(0, orgingIndex);
            int destinationIndex = (int)Session["Destination"].ToString().IndexOf(v);
            string destination = Session["Destination"].ToString().Substring(0, destinationIndex);
            List<Flight> flights = new List<Flight>();
            flights = flightRepo.Filter(destination, origin);

            //Sort the list
            if (Session["SortBy"] != null)
            {
                string sortBy = Session["SortBy"].ToString();
                string classType = Session["classType"].ToString();
                List<Flight> sortedFlights = flightRepo.SortedList(sortBy, classType, flights);
                return View(sortedFlights);
            }
            else
                return View(flights);
        }

        //Button in the Round-Trip Flights List Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookReturnFlight(Flight flight)
        {
            Session["ReturnFlightId"] = flight.id;
            Session["ReturnOrigin"] = flight.origin;
            Session["ReturnDestination"] = flight.destination;
            Session["ReturnDate"] = flight.flight_date;
            return RedirectToAction("BookReturnFlight");
        }

        //Page The contains details of selected return-flights
        public ActionResult BookReturnFlight(int id)
		{
            //Should check if the flight type if its transit or direct
            Flight flight = flightRepo.GetFlightById(id);
            ViewBag.FlightId = id;
            Session["ReturnFlightId"] = id;
            return View(flight);
        }

        //GET: Passenger Form Page
        public ActionResult AddAPassenger()
		{
            ViewBag.FlightId = Session["FlightId"];
            TempData.Keep();
            List<Passenger> passengers = new List<Passenger>();
            passengers.Add(new Passenger());
            return View();
		}

        //POST: Button in the passenger form page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPassenger(Passenger passenger)
        {
			if (ModelState.IsValid)
			{
                double duration;
                //Add passenger to the database
                passengerRepo.Add(passenger);

                //Get the flight id 
                string flightId = Session["FlightId"].ToString();
                Flight flight = flightRepo.GetFlightById(Convert.ToInt32(flightId));

                decimal price = 0;
                //Check what class the user selected
                if (Session["classType"].ToString().Equals("Buisness"))
                    price = (decimal)flight.bus_price;
                else
                    price = (decimal)flight.econ_price;

                //Create a reservation object 
                Reservation reservation = new Reservation();
                //set the value of this reservation
                reservation.reservation_date = DateTime.Now;
                reservation.reservation_status = "Confirmed";
                reservationRepo.Add(reservation);
                Session["ReservationID"] = reservation.id;

                //Connect the flight with the reservation using the Flight_Reservation table
                Flight_Reservation flightReservation = new Flight_Reservation();
                flightReservation.flight_id = flight.id;
                flightReservation.reservation_id = reservation.id;
                flightReservation.price = price;
                flightReservationRepo.Add(flightReservation);

                //Connect the reservation with the passenger using the Passenger_Reservation table
                Passenger_Reservation passReserv = new Passenger_Reservation();
                passReserv.passenger_id = passenger.id;
                passReserv.reservation_id = reservation.id;
                passengerReservationRepo.Add(passReserv);
                Session["PassengerId"] = passenger.id;

                //Reduce the number of available seats in this flight
                flightRepo.BookedSeats(flight.id, 1);

                //Create Itinerary For Going Flight
                Itinerary goingItinerary = new Itinerary();
                goingItinerary.Flight = "FBN-" + flight.id;
                goingItinerary.From = Session["Origin"].ToString();
                goingItinerary.To = Session["Destination"].ToString();
                goingItinerary.Aircraft = flight.aircraft_id;
                goingItinerary.Status = reservation.reservation_status;
                goingItinerary.Passenger_Name = passenger.first_name + " " + passenger.last_name;
                goingItinerary.Ticket_Number = "T-FBN-" + reservation.id;
                goingItinerary.Flyer_Number = passenger.id + "-" + reservation.id + "-" + flight.id + "2021";
                goingItinerary.Special_Needs = "Meal: VGML";
                goingItinerary.Total_Cost = flightReservation.price;
                goingItinerary.From_Date = flight.flight_date;
                duration = (double)flight.duration;
                goingItinerary.To_Date = flight.flight_date.Value.AddHours(duration);
                goingItinerary.Currency = "BHD";
                itineraryRepo.Add(goingItinerary);
                Session["GoingItinerary"] = goingItinerary.id;

                //Check if the user selected round-flight ticket
                var flightType = Session["FlightType"];
                if (flightType != null && Session["FlightType"].ToString() != null && Session["FlightType"].ToString().Equals("Round-Trip"))
                {
                    string returnFlightId = Session["ReturnFlightID"].ToString();
                    Flight returnFlight = flightRepo.GetFlightById(Convert.ToInt32(returnFlightId));
                    //Check what class the user selected
                    if (Session["classType"].ToString().Equals("Buisness"))
                        price = (decimal)returnFlight.bus_price;
                    else
                        price = (decimal)returnFlight.econ_price;
                    FlightReservationRepository returnFlightReservationRepo = new FlightReservationRepository();
                    Flight_Reservation returnFlightReservation = new Flight_Reservation();
                    returnFlightReservation.flight_id = returnFlight.id;
                    returnFlightReservation.reservation_id = reservation.id;
                    returnFlightReservation.price = price;
                    returnFlightReservationRepo.Add(returnFlightReservation);
                    //Reduce the number of available seats in this flight
                    flightRepo.BookedSeats(returnFlight.id, 1);
                    Session["ReturnFlightID"] = returnFlight.id;

                    Itinerary returnItinerary = new Itinerary();
                    returnItinerary.Flight = "FBN-" + returnFlight.id;
                    returnItinerary.From = Session["Destination"].ToString();
                    returnItinerary.To = Session["Origin"].ToString();
                    returnItinerary.Aircraft = returnFlight.aircraft_id;
                    returnItinerary.Status = reservation.reservation_status;
                    returnItinerary.Passenger_Name = passenger.first_name + " " + passenger.last_name;
                    returnItinerary.Ticket_Number = "T-FBN-" + reservation.id;
                    returnItinerary.Flyer_Number = passenger.id + "-" + reservation.id + "-" + flight.id + "2021";
                    returnItinerary.Special_Needs = "Meal: VGML";
                    returnItinerary.Total_Cost = returnFlightReservation.price;
                    returnItinerary.From_Date = returnFlight.flight_date;
                    duration = (double)returnFlight.duration;
                    returnItinerary.To_Date = returnFlight.flight_date.Value.AddHours(duration);
                    returnItinerary.Currency = "BHD";
                    itineraryRepo.Add(returnItinerary);
                    Session["ReturnItinerary"] = returnItinerary.id;
                }

                //Check if the current user is a registered or logged in user
                if (User.Identity.IsAuthenticated)
                {
                    // Add this reservation to the user reservation history
                    string userId = UserHelper.GetCurrentUserId();
                    Customer cusotmer = customerRepo.GetCustomerByUserId(userId);

                    Reservation_History history = new Reservation_History();
                    history.customer_id = cusotmer.id;
                    history.reservation_id = reservation.id;
                    //Add this history to the db
                    historyRepo.Add(history);
                }

                TempData["SuccessMessage"] = "Flight was booked successfully";
                TempData.Keep();
                return RedirectToAction("ConfirmationItinerary");
			}
			else
			{
                return View(passenger);
			}
            
        }

        //Confirm Page Which Displays E-Ticket
        public ActionResult ConfirmationItinerary()
		{
            string itineraryId = Session["GoingItinerary"].ToString();
            List<Itinerary> itineraries = new List<Itinerary>();
            Itinerary itinerary = itineraryRepo.GetItineraryById(Convert.ToInt32(itineraryId));
            itineraries.Clear();
            itineraries.Add(itinerary);
            var flightType = Session["FlightType"];
            if (flightType != null && Session["FlightType"].ToString().Equals("Round-Trip"))
			{
                string returnItineraryId = Session["ReturnItinerary"].ToString();
                Itinerary returnItinerary = itineraryRepo.GetItineraryById(Convert.ToInt32(returnItineraryId));
                itineraries.Add(returnItinerary);
            }
            return View(itineraries);
		}
	}
}
