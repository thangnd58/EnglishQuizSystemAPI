using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnglishQuizSystemClient.Controllers
{
	public class AuthController : Controller
	{
		protected string apiLink = "https://localhost:7280/api/Auth";
		protected string apiLinkUser = "https://localhost:7280/api/User";

		public IActionResult Login()
		{
			ViewBag.Message = "";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			var auth = new
			{
				UserName = username,
				Password = password
			};
			try
			{
				using (HttpClient client = new HttpClient())
				{
					using (HttpResponseMessage res = await client.PostAsJsonAsync(apiLink, auth))
					{
						using (HttpContent content = res.Content)
						{
							if (res.IsSuccessStatusCode)
							{
								string data = await content.ReadAsStringAsync();
								// Deserialize the JSON response
								JObject jsonData = JsonConvert.DeserializeObject<JObject>(data);
								// Access properties of the JSON object as needed
								string token = jsonData["token"].ToString();
								HttpContext.Session.SetString("token", token);
								return RedirectToAction("Index", "Home");
							}
							else
							{
								ViewBag.Message = "Invalid user name or password!";
								return View();
							};
						}
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Error server!";
				return View();
			}
		}

		public IActionResult Register()
		{
			ViewBag.Message = "";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(string username, string password, string cf_password)
		{
			if (password != cf_password)
			{
				ViewBag.Message = "Invalid confirm password!";
				return View();
			}
			else
			{
				var auth = new
				{
					UserName = username,
					Password = password,
					RoleId = 1
				};

				try
				{
					using (HttpClient client = new HttpClient())
					{
						using (HttpResponseMessage res = await client.PostAsJsonAsync(apiLinkUser, auth))
						{
							using (HttpContent content = res.Content)
							{
								if (res.IsSuccessStatusCode)
								{
									return RedirectToAction("Login", "Auth");
								}
								else
								{
									ViewBag.Message = "Cannot register!";
									return View();
								};
							}
						}
					}
				}
				catch (Exception ex)
				{
					ViewBag.Message = "Error server!";
					return View();
				}
			}
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("token");
			return RedirectToAction("Index", "Home");
		}
	}
}
