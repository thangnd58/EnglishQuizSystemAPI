using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using EnglishQuizSystemClient.Models;
using System.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace EnglishQuizSystemClient.Controllers
{
	public class QuizController : Controller
	{
		protected string apiLinkQuestion = "https://localhost:7280/api/Question";
		protected string apiLinkAnswer = "https://localhost:7280/api/Answer";
        protected string apiLinkQuiz = "https://localhost:7280/api/Quiz";

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int id)
		{
			List<Question> questionList = new List<Question>();
            Quiz quiz = new Quiz();
            string token = HttpContext.Session.GetString("token");
            try
			{
				using (HttpClient client = new HttpClient())
				{
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using (HttpResponseMessage res = await client.GetAsync(apiLinkQuestion + "?$filter=QuizId eq " + id))
					{
						using (HttpContent content = res.Content)
						{
							if (res.IsSuccessStatusCode)
							{
								string data = await content.ReadAsStringAsync();
								questionList = JsonConvert.DeserializeObject<List<Question>>(data);
								ViewBag.ListQuestion = questionList;
							}
							else
							{
								ViewBag.Message = "Please login!";
								return View();
							};
						}
					}

                    using (HttpResponseMessage res = await client.GetAsync(apiLinkQuiz + "/" + id))
                    {
                        using (HttpContent content = res.Content)
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                string data = await content.ReadAsStringAsync();
                                quiz = JsonConvert.DeserializeObject<Quiz>(data);
                                ViewBag.Quiz = quiz;
                                
                            }
                            else
                            {
                                ViewBag.Message = "Please login!";
                                return View();
                            };
                        }
                    }

                    foreach (var q in questionList)
					{
                        List<Answer> answerList = new List<Answer>();
                        using (HttpResponseMessage res = await client.GetAsync(apiLinkAnswer + "?$filter=QuestionId eq " + q.Id))
                        {
                            using (HttpContent content = res.Content)
                            {
                                if (res.IsSuccessStatusCode)
                                {
                                    string data = await content.ReadAsStringAsync();
                                    answerList = JsonConvert.DeserializeObject<List<Answer>>(data);
									q.Answers = answerList;
                                }
                                else
                                {
                                    ViewBag.Message = "Please login!";
                                    return View();
                                };
                            }
                        }
                    }
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Error server: " + ex.Message;
				return View();
			}
            return View();
        }

		//TO-DO
		[HttpPost]
		public async Task<IActionResult> Submit(IFormCollection form)
		{
            Dictionary<int, List<int>> listQuestionAnswer = new Dictionary<int, List<int>>();

            var qusOneValue = form["qusOne"];
            var qusMulValue = form["qusMul"];
            var quizId = form["quizId"];

            foreach (var qus in qusOneValue.Concat(qusMulValue))
            {
                string[] quesAns = qus.Split(' ');
                int[] ints = Array.ConvertAll(quesAns, s => int.Parse(s));

                if (!listQuestionAnswer.TryGetValue(ints[0], out List<int> answerList))
                {
                    listQuestionAnswer[ints[0]] = answerList = new List<int>();
                }
                answerList.Add(ints[1]);
            }
            int i = 1;
            return View();
        }
	}
}
