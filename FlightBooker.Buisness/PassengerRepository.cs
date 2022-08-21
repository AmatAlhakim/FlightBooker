using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class PassengerRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Passenger> GetAllPassengers()
		{
			List<Passenger> passengers = context.Passengers.ToList();
			return passengers;
		}

		public Passenger GetPassengerById(int id)
		{
			Passenger passenger = context.Passengers.Find(id);
			return passenger;
		}

		public void Add(Passenger passenger)
		{
			try
			{
				context.Passengers.Add(passenger);
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
				Passenger passenger = context.Passengers.Find(id);
				context.Passengers.Remove(passenger);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Passenger passenger)
		{
			try
			{
				Passenger oldPassenger = context.Passengers.Find(passenger.id);
				context.Entry(oldPassenger).CurrentValues.SetValues(passenger);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public int TotalPassengers()
		{
			int total = context.Passengers.Count();
			return total;
		}
	}
}
