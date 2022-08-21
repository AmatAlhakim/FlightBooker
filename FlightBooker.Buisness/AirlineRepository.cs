using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class AirlineRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Airline> GetAllAirlines()
		{
			List<Airline> airlines = context.Airlines.ToList();
			return airlines;
		}

		public Airline GetAirlineById(int id)
		{
			Airline airline = context.Airlines.Find(id);
			return airline;
		}

		public void Add(Airline airline)
		{
			try
			{
				context.Airlines.Add(airline);
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
				Airline airline = context.Airlines.Find(id);
				context.Airlines.Remove(airline);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Airline airline)
		{
			try
			{
				Airline oldAirline = context.Airlines.Find(airline.id);
				context.Entry(oldAirline).CurrentValues.SetValues(airline);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
