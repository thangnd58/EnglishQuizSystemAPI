using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using EnglishQuizSystemClient.Models;
using System.Net.Http.Headers;

namespace EnglishQuizSystemClient.Controllers
{
    public class QuizController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiLinkQuestion = "https://localhost:7280/api/Question";
        private const string ApiLinkAnswer = "https://localhost:7280/api/Answer";
        private const string ApiLinkQuiz = "https://localhost:7280/api/Quiz";
        private const string ApiLinkUser = "https://localhost:7280/api/User";
        private const string ApiLinkUserAnswer = "https://localhost:7280/api/UserAnswer";

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                var username = HttpContext.Session.GetString("username");
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
                {
                    ViewBag.Message = "Please login!";
                    return View();
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var user = await GetUserByNameAsync(username);
                if (user == null)
                {
                    ViewBag.Message = "User not found!";
                    return View();
                }

                var userAnswers = await GetListUserAnswer(id, user.Id);
                if (userAnswers != null && userAnswers.Count > 0)
                {
                    ViewBag.ListUserAnswer = userAnswers;
                }

                var questionList = await GetQuestionsByQuizIdAsync(id);
                if (questionList == null)
                {
                    ViewBag.Message = "Error load question!";
                    return View();
                }

                var quiz = await GetQuizByIdAsync(id);
                if (quiz == null)
                {
                    ViewBag.Message = "Error load quiz!";
                    return View();
                }

                foreach (var question in questionList)
                {
                    var answerList = await GetAnswersByQuestionIdAsync(question.Id);
                    if (answerList == null)
                    {
                        ViewBag.Message = "Error load answer list!";
                        return View();
                    }
                    question.Answers = answerList;
                }

                ViewBag.ListQuestion = questionList;
                ViewBag.Quiz = quiz;
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error server: " + ex.Message;
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IFormCollection form)
        {
            try
            {
                var username = HttpContext.Session.GetString("username");
                var token = HttpContext.Session.GetString("token");
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
                {
                    ViewBag.Message = "Please login!";
                    return View();
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var qusOneValue = form["qusOne"];
                var qusMulValue = form["qusMul"];
                var quizId = int.Parse(form["quizId"]);

                var user = await GetUserByNameAsync(username);
                if (user == null)
                {
                    ViewBag.Message = "User not found!";
                    return View();
                }

                var listQuestionAnswer = new Dictionary<int, List<int>>();
                foreach (var qus in qusOneValue.Concat(qusMulValue))
                {
                    var quesAns = qus.Split(' ');
                    var ints = Array.ConvertAll(quesAns, s => int.Parse(s));

                    if (!listQuestionAnswer.TryGetValue(ints[0], out List<int> answerList))
                    {
                        listQuestionAnswer[ints[0]] = answerList = new List<int>();
                    }
                    answerList.Add(ints[1]);
                }

                var listUserAnswers = new List<UserAnswer>();
                foreach (var i in listQuestionAnswer)
                {
                    foreach (var answer in i.Value)
                    {
                        var userAnswer = new UserAnswer
                        {
                            AnswerId = answer,
                            UserId = user.Id,
                            QuizId = quizId,
                            QuestionId = i.Key
                        };
                        listUserAnswers.Add(userAnswer);
                    }
                }

                var response = await _httpClient.PostAsJsonAsync(ApiLinkUserAnswer, listUserAnswers);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Error submited!";
                    return View();
                }

                ViewBag.Message = "Submitted! Back to home to view result.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error server: " + ex.Message;
                return View();
            }
        }

        private async Task<List<Question>> GetQuestionsByQuizIdAsync(int id)
        {
            using (var res = await _httpClient.GetAsync($"{ApiLinkQuestion}?$filter=QuizId eq {id}"))
            {
                using (var content = res.Content)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Question>>(data);
                    }
                    return null;
                }
            }
        }

        private async Task<Quiz> GetQuizByIdAsync(int id)
        {
            using (var res = await _httpClient.GetAsync($"{ApiLinkQuiz}/{id}"))
            {
                using (var content = res.Content)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Quiz>(data);
                    }
                    return null;
                }
            }
        }

        private async Task<List<Answer>> GetAnswersByQuestionIdAsync(int id)
        {
            using (var res = await _httpClient.GetAsync($"{ApiLinkAnswer}?$filter=QuestionId eq {id}"))
            {
                using (var content = res.Content)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Answer>>(data);
                    }
                    return null;
                }
            }
        }

        private async Task<List<UserAnswer>> GetListUserAnswer(int quizId, int userId)
        {
            using (var res = await _httpClient.GetAsync($"{ApiLinkUserAnswer}?$filter=UserId eq {userId} and QuizId eq {quizId}"))
            {
                using (var content = res.Content)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<UserAnswer>>(data);
                    }
                    return null;
                }
            }
        } 

        private async Task<User> GetUserByNameAsync(string username)
        {
            using (var res = await _httpClient.GetAsync($"{ApiLinkUser}/{username}"))
            {
                using (var content = res.Content)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<User>(data);
                    }
                    return null;
                }
            }
        }
    }
}