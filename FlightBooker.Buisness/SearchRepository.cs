using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class SearchRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Search> GetAllSearches()
		{
			List<Search> searches = context.Searches.ToList();
			return searches;
		}

		public Search GetSearchById(int id)
		{
			Search searche = context.Searches.Find(id);
			return searche;
		}

		public void Add(Search search)
		{
			try
			{
				context.Searches.Add(search);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			try
			{
				Search search = context.Searches.Find(id);
				context.Searches.Remove(search);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			context.SaveChanges();
		}

		public void Update(Search search)
		{
			try
			{
				Search oldSearch = context.Searches.Find(search.id);
				context.Entry(oldSearch).CurrentValues.SetValues(search);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			context.SaveChanges();
		}
	}
}
