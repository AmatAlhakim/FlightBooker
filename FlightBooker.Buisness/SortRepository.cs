using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class SortRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Sort> GetAllSortTypes()
		{
			List<Sort> Sortes = context.Sorts.ToList();
			return Sortes;
		}

		public Sort GetSortTypeById(int id)
		{
			Sort Sorte = context.Sorts.Find(id);
			return Sorte;
		}

		public void Add(Sort Sort)
		{
			try
			{
				context.Sorts.Add(Sort);

			}catch(Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			try
			{
				Sort Sort = context.Sorts.Find(id);
				context.Sorts.Remove(Sort);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			context.SaveChanges();
		}

		public void Update(Sort Sort)
		{
			try
			{
				Sort oldSort = context.Sorts.Find(Sort.id);
				context.Entry(oldSort).CurrentValues.SetValues(Sort);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
			context.SaveChanges();
		}


	}
}
