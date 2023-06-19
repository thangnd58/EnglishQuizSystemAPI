using EnglishQuizSystemClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EnglishQuizSystemClient.Controllers
{
	public class HomeController : Controller
	{
        private const string ApiLinkUserAnswer = "https://localhost:7280/api/UserAnswer";
        private const string ApiLinkUser = "https://localhost:7280/api/User";
        private readonly HttpClient _httpClient = new HttpClient();


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