using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class AspNetUserRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<AspNetUser> GetAllUsers()
		{
			List<AspNetUser> users = context.AspNetUsers.ToList();
			return users;
		}

		public AspNetUser GetUserById(string id)
		{
			AspNetUser user = context.AspNetUsers.Find(id);
			return user;
		}

		public void Add(AspNetUser user)
		{
			try
			{
				context.AspNetUsers.Add(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Delete(string id)
		{
			try
			{
				AspNetUser user = context.AspNetUsers.Find(id);
				context.AspNetUsers.Remove(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(AspNetUser user)
		{
			try
			{
				AspNetUser oldUser = context.AspNetUsers.Find(user.Id);
				context.Entry(oldUser).CurrentValues.SetValues(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}


	}
}
