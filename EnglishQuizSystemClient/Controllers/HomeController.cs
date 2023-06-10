using EnglishQuizSystemClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnglishQuizSystemClient.Controllers
{
	public class HomeController : Controller
	{
        protected string apiLink = "http://localhost:7280/api/Quiz";

        public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}