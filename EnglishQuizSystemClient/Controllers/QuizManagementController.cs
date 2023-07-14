using Microsoft.AspNetCore.Mvc;

namespace EnglishQuizSystemClient.Controllers
{
	public class QuizManagementController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
