using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class CustomerRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Customer> GetAllCustomers()
		{
			List<Customer> customers = context.Customers.ToList();
			return customers;
		}

		public Customer GetCustomerById(int id)
		{
			Customer customer = context.Customers.Find(id);
			return customer;
		}

		public Customer GetCustomerByUserId(string id)
		{
			Customer customer = context.Customers.Where(p => p.user_id == id).FirstOrDefault();
			return customer;
		}

		public void Add(Customer customer)
		{
			try
			{
				context.Customers.Add(customer);
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
				Customer customer = context.Customers.Find(id);
				context.Customers.Remove(customer);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Customer customer)
		{
			try
			{
				Customer oldCustomer = context.Customers.Find(customer.id);
				context.Entry(oldCustomer).CurrentValues.SetValues(customer);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
