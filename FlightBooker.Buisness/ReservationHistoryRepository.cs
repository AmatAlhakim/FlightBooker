using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class ReservationHistoryRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Reservation_History> GetAllReservationHistories()
		{
			List<Reservation_History> reservations = context.Reservation_History.ToList();
			return reservations;
		}

		public Reservation_History GetReservationHistoryById(int id)
		{
			Reservation_History reservation = context.Reservation_History.Find(id);
			return reservation;
		}

		public Reservation_History GetReservationHistoryByUserId(int id)
		{
			Reservation_History reservation = context.Reservation_History.Where(p => p.customer_id == id).FirstOrDefault();
			return reservation;
		}

		public List<Reservation_History> GetAllReservationHistoryByUserId(int id)
		{
			List<Reservation_History> reservations = context.Reservation_History.Where(p => p.customer_id == id).ToList();
			return reservations;
		}

		public void Add(Reservation_History reservation)
		{
			try
			{
				context.Reservation_History.Add(reservation);
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
				Reservation_History reservation = context.Reservation_History.Find(id);
				context.Reservation_History.Remove(reservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Reservation_History reservation)
		{
			try
			{
				Reservation_History oldReservation = context.Reservation_History.Find(reservation.id);
				context.Entry(oldReservation).CurrentValues.SetValues(reservation);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public int TotalNumOfReservations()
		{
			int total = context.Flight_Reservation.Count();
			return total;
		}
	}
}

