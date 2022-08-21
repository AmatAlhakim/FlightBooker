using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;
using FlightBooker.Buisness;

namespace FlightBooker.Buisness
{
	public class ClassTypeRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<ClassType> GetAllClassTypes()
		{
			List<ClassType> types = context.ClassTypes.ToList();
			return types;
		}

		public ClassType GetClassTypeById(int id)
		{
			ClassType type = context.ClassTypes.Find(id);
			return type;
		}

		public void Add(ClassType type)
		{
			try
			{
				context.ClassTypes.Add(type);
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
				ClassType type = context.ClassTypes.Find(id);
				context.ClassTypes.Remove(type);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(ClassType type)
		{
			try
			{
				ClassType oldType = context.ClassTypes.Find(type.id);
				context.Entry(oldType).CurrentValues.SetValues(type);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
