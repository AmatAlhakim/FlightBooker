using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class AirportRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Airport> GetAllAirports()
		{
			List<Airport> airports = context.Airports.ToList();
			return airports;
		}

		public Airport GetAirportById(int id)
		{
			Airport airport = context.Airports.Find(id);
			return airport;
		}

		public void Add(Airport airport)
		{
			try
			{
				context.Airports.Add(airport);
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
				Airport airport = context.Airports.Find(id);
				context.Airports.Remove(airport);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Airport airport)
		{
			try
			{
				Airport oldAirport = context.Airports.Find(airport.id);
				context.Entry(oldAirport).CurrentValues.SetValues(airport);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
