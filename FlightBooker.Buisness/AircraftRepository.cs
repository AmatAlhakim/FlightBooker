using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class AircraftRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Aircraft> GetAllAircrafts()
		{
			List<Aircraft> aircrafts = context.Aircraft.ToList();
			return aircrafts;
		}

		public Aircraft GetAircraftById(int id)
		{
			Aircraft aircraft = context.Aircraft.Find(id);
			return aircraft;
		}

		public void Add(Aircraft aircraft)
		{
			try
			{
				context.Aircraft.Add(aircraft);
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
				Aircraft aircraft = context.Aircraft.Find(id);
				context.Aircraft.Remove(aircraft);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Aircraft aircraft)
		{
			try
			{
				Aircraft oldAircraft = context.Aircraft.Find(aircraft.id);
				context.Entry(oldAircraft).CurrentValues.SetValues(aircraft);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
