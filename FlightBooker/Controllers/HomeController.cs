using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
namespace FlightBooker.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

			if (User.IsInRole("Admin") && UserHelper.count == 0)
			{
				TempData["CurrentUserRole"] = "Admin";
				return RedirectToAction("Index", "Admin", new { area = "Admin" });
			}

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[HttpPost]
		public ActionResult Details()
		{

			return Redirect(Url.Content("Home/Contact"));
		}
	}
}