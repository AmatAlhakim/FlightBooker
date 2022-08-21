using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooker.DAL;

namespace FlightBooker.Buisness
{
	public class PaymentDetailsRepository
	{
		FlightBookerDb context = new FlightBookerDb();

		public List<Payment_Details> GetAllPaymentDetails()
		{
			List<Payment_Details> paymentDetails = context.Payment_Details.ToList();
			return paymentDetails;
		}

		public Payment_Details GetPaymentDetailById(int id)
		{
			Payment_Details paymentDetail = context.Payment_Details.Find(id);
			return paymentDetail;
		}

		public void Add(Payment_Details paymentDetail)
		{
			try
			{
				context.Payment_Details.Add(paymentDetail);
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
				Payment_Details paymentDetail = context.Payment_Details.Find(id);
				context.Payment_Details.Remove(paymentDetail);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}

		public void Update(Payment_Details paymentDetail)
		{
			try
			{
				Payment_Details oldPayment_Detail = context.Payment_Details.Find(paymentDetail.id);
				context.Entry(oldPayment_Detail).CurrentValues.SetValues(paymentDetail);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			context.SaveChanges();
		}
	}
}
