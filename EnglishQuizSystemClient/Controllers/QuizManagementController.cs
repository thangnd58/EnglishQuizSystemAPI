using EnglishQuizSystemClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EnglishQuizSystemClient.Controllers
{
	public class QuizManagementController : Controller
	{
		private readonly HttpClient _httpClient = new HttpClient();
		private const string ApiLinkQuestion = "https://localhost:7280/api/Question";
		private const string ApiLinkQuiz = "https://localhost:7280/api/Quiz";
		public async Task<IActionResult> IndexAsync(int id)
		{
			var token = HttpContext.Request.Cookies["token"];
			var username = HttpContext.Request.Cookies["username"];
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
			{
				return View();
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var listQuestion = new List<Question>();
			using (var res = await _httpClient.GetAsync($"{ApiLinkQuestion}?$filter=QuizId eq {id}&expand=Answers"))
			{
				using (var content = res.Content)
				{
					if (res.IsSuccessStatusCode)
					{
						var data = await content.ReadAsStringAsync();
						listQuestion = JsonConvert.DeserializeObject<List<Question>>(data);
					}
				}
			}
			var quiz = new Quiz();
			using (var res = await _httpClient.GetAsync($"{ApiLinkQuiz}/{id}"))
			{
				using (var content = res.Content)
				{
					if (res.IsSuccessStatusCode)
					{
						var data = await content.ReadAsStringAsync();
						quiz = JsonConvert.DeserializeObject<Quiz>(data);
					}
				}
			}
			ViewBag.Questions = listQuestion;
			ViewBag.Quiz = quiz;
			return View();
		}

		public async Task<IActionResult> EditAsync(IFormCollection form)
		{
			var token = HttpContext.Request.Cookies["token"];
			var username = HttpContext.Request.Cookies["username"];
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
			{
				return View();
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var quizId = form["quizId"];
			var quizName = form["quizName"];
			var quizActive = form["quizActive"] == "on" ? true : false;
			Quiz q = new Quiz();
			q.Active = quizActive;
			q.Name = quizName;
			q.CodeActive = "123456";

			using (var res = await _httpClient.PutAsJsonAsync($"{ApiLinkQuiz}/{quizId}", q))
			{
				using (var content = res.Content)
				{
					if (res.IsSuccessStatusCode)
					{
						return RedirectToAction("Index", "QuizManagement", new { id = quizId });
					}
				}
			}
			return RedirectToAction("Index", "QuizManagement", new { id = quizId });
		}
	}
}
