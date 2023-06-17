using AutoMapper;
using EnglishQuizSystem.DTO;
using EnglishQuizSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Data;

namespace EnglishQuizSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswerController : ControllerBase
    {
        private readonly EnglishQuizSystemContext _context;
        private readonly IMapper _mapper;

        public UserAnswerController(EnglishQuizSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                using (_context)
                {
                    var listAllUserAnswers = _context.UserAnswers.ToList();
                    return (listAllUserAnswers == null ? NotFound() : Ok(_mapper.Map<IEnumerable<UserAnswerDTO>>(listAllUserAnswers)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Not found:" + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Post([FromBody] IEnumerable<UserAnswerDTO> userAnswerDTO)
        {
            try
            {
                using (_context)
                {
                    _context.UserAnswers.AddRange(_mapper.Map<IEnumerable<UserAnswer>>(userAnswerDTO));
                    _context.SaveChanges();
                    UserAnswerDTO temp = userAnswerDTO.First();
                    GradeForUser(temp.QuizId, temp.QuizId);
                    return Ok("Insert Successfully.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGrade")]
        public IActionResult GetGrade(int quizId, int userId)
        {
            try
            {
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //TO_DO
        private void GradeForUser(int quizId, int userId)
        {
            using (_context)
            {
                int points = 0;
                var userAnswers = _context.UserAnswers
                    .Where(x => x.UserId == userId && x.QuizId == quizId)
                    .ToList();

                var keyValuePairs = userAnswers
                    .GroupBy(x => x.QuestionId)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.AnswerId).ToList());
                var listAnswers = _context.Answers.ToList();
                foreach (var a in keyValuePairs.Values)
                {

                }
                UserQuiz uq = new UserQuiz();
                uq.UserId = userId;
                uq.QuizId = quizId;
                uq.Score = points;
                _context.UserQuizzes.Add(uq);
                _context.SaveChanges();
            }
        }
    }

}
