using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using FlightBooker.Models;
using FlightBooker.DAL;

namespace FlightBooker
{
	public class UserHelper
	{

		public static int count = 0;
		public static int flightID = 0;
		public static Flight selectedFlight;
		//get the id of the current user 
		//can be used when creating customer reservation
		public static string GetCurrentUserId()
		{
			return System.Web.HttpContext.Current.User.Identity.GetUserId();
		}
		public static string GetUserNameById(string userId)
		{
			ApplicationUser user = HttpContext
									.Current
									.GetOwinContext()
									.GetUserManager<ApplicationUserManager>()
									.FindById(userId);
			if (user != null)
			{
				return user.UserName;
			}
			return "";
		}

	}
}