using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class ReservationRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Reservation> GetAllReservations()
		{
			List<Reservation> reservations = context.Reservations.ToList();
			return reservations;
		}

		public Reservation GetReservationById(int id)
		{
			Reservation reservation = context.Reservations.Find(id);
			return reservation;
		}

		public void Add(Reservation reservation)
		{
			try
			{
				context.Reservations.Add(reservation);
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
				Reservation reservation = context.Reservations.Find(id);
				context.Reservations.Remove(reservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Reservation reservation)
		{
			try
			{
				Reservation oldReservation = context.Reservations.Find(reservation.id);
				context.Entry(oldReservation).CurrentValues.SetValues(reservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public int TotalNumOfReservation()
		{
			int total = (int) context.Reservations.Count();
			return total;
		}
	}
}
