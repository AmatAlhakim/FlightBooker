using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class ItineraryRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Itinerary> GetAllItinerary()
		{
			List<Itinerary> itineraries = context.Itineraries.ToList();
			return itineraries;
		}

		public Itinerary GetItineraryById(int id)
		{
			Itinerary itinerary = context.Itineraries.Find(id);
			return itinerary;
		}

		public void Add(Itinerary itinerary)
		{
			try
			{
				context.Itineraries.Add(itinerary);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			try
			{
				Itinerary itinerary = context.Itineraries.Find(id);
				context.Itineraries.Remove(itinerary);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Itinerary itinerary)
		{
			try
			{
				Itinerary oldItinerary  = context.Itineraries.Find(itinerary.id);
				context.Entry(oldItinerary).CurrentValues.SetValues(itinerary);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

	}
}
