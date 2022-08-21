using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class FlightReservationRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Flight_Reservation> GetAllFlightReservations()
		{
			List<Flight_Reservation> flightReservations = context.Flight_Reservation.ToList();
			return flightReservations;
		}

		public Flight_Reservation GetFlightReservationById(int id)
		{
			Flight_Reservation flightReservation = context.Flight_Reservation.Find(id);
			return flightReservation;
		}

		public void Add(Flight_Reservation flightReservation)
		{
			try
			{
				context.Flight_Reservation.Add(flightReservation);
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
				Flight_Reservation flightReservation = context.Flight_Reservation.Find(id);
				context.Flight_Reservation.Remove(flightReservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Flight_Reservation flightReservation)
		{
			try
			{
				Flight_Reservation oldFlightReservation = context.Flight_Reservation.Find(flightReservation.id);
				context.Entry(oldFlightReservation).CurrentValues.SetValues(flightReservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public double TotalPrice()
		{
			double total = 0;
			foreach (var item in context.Flight_Reservation.ToList())
			{
				total += (double) item.price;
			}
			return total;
		}
	}
}
