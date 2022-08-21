using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class PaymentRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Payment> GetAllPayments()
		{
			List<Payment> payments = context.Payments.ToList();
			return payments;
		}

		public Payment GetPaymentById(int id)
		{
			Payment payment = context.Payments.Find(id);
			return payment;
		}

		public void Add(Payment payment)
		{
			try
			{
				context.Payments.Add(payment);
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
				Payment payment = context.Payments.Find(id);
				context.Payments.Remove(payment);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Payment payment)
		{
			try
			{
				Payment oldPayment = context.Payments.Find(payment.id);
				context.Entry(oldPayment).CurrentValues.SetValues(payment);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
