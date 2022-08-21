using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class CountryRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Country> GetAllCountries()
		{
			List<Country> countries = context.Countries.ToList();
			return countries;
		}

		public Country GetCountryById(int id)
		{
			Country country = context.Countries.Find(id);
			return country;
		}

		public void Add(Country country)
		{
			try
			{
				context.Countries.Add(country);
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
				Country country = context.Countries.Find(id);
				context.Countries.Remove(country);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Country country)
		{
			try
			{
				Country oldCountry = context.Countries.Find(country.id);
				context.Entry(oldCountry).CurrentValues.SetValues(country);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}

