using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class PassengerReservationRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Passenger_Reservation> GetAllPassengerReservations()
		{
			List<Passenger_Reservation> passengerReservations = context.Passenger_Reservation.ToList();
			return passengerReservations;
		}

		public Passenger_Reservation GetPassengerReservationById(int id)
		{
			Passenger_Reservation passengerReservation = context.Passenger_Reservation.Find(id);
			return passengerReservation;
		}

		public void Add(Passenger_Reservation passengerReservation)
		{
			try
			{
				context.Passenger_Reservation.Add(passengerReservation);
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
				Passenger_Reservation passengerReservation = context.Passenger_Reservation.Find(id);
				context.Passenger_Reservation.Remove(passengerReservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Passenger_Reservation passengerReservation)
		{
			try
			{
				Passenger_Reservation oldPassengerReservation = context.Passenger_Reservation.Find(passengerReservation.id);
				context.Entry(oldPassengerReservation).CurrentValues.SetValues(passengerReservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
