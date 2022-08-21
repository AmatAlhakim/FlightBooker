using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;
using System.Collections;

namespace FlightBooker.Buisness
{
	public class FlightRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Flight> GetAllFlights()
		{
			List<Flight> flights = context.Flights.ToList();
			return flights;
		}

		public Flight GetFlightById(int id)
		{
			Flight flight = context.Flights.Find(id);
			return flight;
		}

		public void Add(Flight flight)
		{
			try
			{
				context.Flights.Add(flight);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			try
			{
				Flight flight = context.Flights.Find(id);
				context.Flights.Remove(flight);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Flight flight)
		{
			try
			{
				Flight oldFlight = context.Flights.Find(flight.id);
				context.Entry(oldFlight).CurrentValues.SetValues(flight);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		//Reduce The number of Seats in a flight based on the number of passengers
		public void BookedSeats(int flightId, int passengerNo)
		{
			//1- Get the flight
			Flight flight = GetFlightById(flightId);
			// 2- Update the total number of seats
			int currentNo = (int) flight.available_seats;
			int seatsNo = currentNo - passengerNo;
			flight.available_seats = (int) seatsNo;
			// Update the flight data in the database
			Update(flight);
		}

		//Used when a reservation is canceled
		public void CancelBookedSeats(int flightId, int passengerNo)
		{
			//1- Get the flight
			Flight flight = GetFlightById(flightId);
			// 2- Update the total number of seats
			int currentNo = (int)flight.available_seats;
			int seatsNo = currentNo + passengerNo;
			flight.available_seats = (int)seatsNo;
			// Update the flight data in the database
			Update(flight);
		}

		//Filter methods that will be used for search and sort functionalities
		//filter based on origin and destination
		public List<Flight> Filter(string origin, string destination)
		{
			List<Flight> flights = context
									.Flights
									.Where( p => p.origin == origin && p.destination == destination)
									.ToList();
			return flights;
		}

		//filter based on origin, destination and date
		public List<Flight> Filter(string origin, string destination, DateTime date)
		{
			List<Flight> flights = context
									.Flights
									.Where(p => p.origin == origin && p.destination == destination && p.flight_date == date)
									.ToList();
			return flights;
		}

		//Filter Method to allow the user to search for all flights based on a selected date
		public List<Flight> Filter(DateTime date)
		{
			List<Flight> flights = context
									.Flights
									.Where(p => p.flight_date == date)
									.ToList();
			return flights;
		}

		//Filter method for populating a list of flights that will stop over a third country
		//This method will be used for the trnsmit and transfor fligts
		public List<Flight> Filter(string origin, string destination, string via)
		{
			List<Flight> flights = context
									.Flights
									.Where(p => p.origin == origin && 
											p.destination == destination && 
											p.via == via)
									.ToList();
			return flights;
		}

		//Filter method for populating a list of flights that will stop over a third country
		//This method will be used for the trnsmit and transfor fligts
		public List<Flight> Filter(string origin, string destination, DateTime date, string via)
		{
			List<Flight> flights = context
									.Flights
									.Where(p => p.origin == origin &&
											p.destination == destination &&
											p.flight_date == date &&
											p.via == via)
									.ToList();
			List<Flight> viaList = context.Flights.Where(p => p.origin == via).ToList();

			flights.AddRange(viaList);
			return flights;
		}

		public List<Flight> SortedList (string sortBy, string classType, List<Flight> flights)
		{
			List<Flight> sortedList = new List<Flight>();
			IEnumerable<Flight> sortedFlights;

			//Check Class Type
			if (classType.Equals("Buisness"))
			{
				if (sortBy.Equals("Lowest Price"))
				{
					sortedFlights = flights.OrderBy(p => p.bus_price);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
				else if (sortBy.Equals("Highest Price"))
				{
					sortedFlights = flights.OrderByDescending(p => p.bus_price);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
				else if (sortBy.Equals("Longest"))
				{
					sortedFlights = flights.OrderByDescending(p => p.duration);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
				else
				{
					sortedFlights = flights.OrderBy(p => p.duration);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
			}
			else
			{
				if (sortBy.Equals("Lowest Price"))
				{
					sortedFlights = flights.OrderBy(p => p.bus_price);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
				else if (sortBy.Equals("Highest Price"))
				{
					sortedFlights = flights.OrderByDescending(p => p.bus_price);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
				else if (sortBy.Equals("Longest"))
				{
					sortedFlights = flights.OrderByDescending(p => p.duration);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
				else
				{
					sortedFlights = flights.OrderBy(p => p.duration);
					sortedList = sortedFlights.ToList();
					return sortedList;
				}
			}

			
		}

		//Method to calculate the flight costs
		public double TotalValue(int id, string flightType)
		{
			try
			{
				double totalValue = 0;
				Flight flight = context.Flights.Find(id);
				if (flightType.Equals("econ_price", StringComparison.InvariantCultureIgnoreCase))
					totalValue = Convert.ToDouble(flight.econ_price);
				else if (flightType.Equals("bus_price", StringComparison.InvariantCultureIgnoreCase))
					totalValue = Convert.ToDouble(flight.econ_price);
				return totalValue;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		//calculate the total number of flights available in the db
		//This method will be used in the admin dashboard for reports
		public int TotalCount()
		{
			int count = context.Flights.Count();
			return count;
		}
	}
}

