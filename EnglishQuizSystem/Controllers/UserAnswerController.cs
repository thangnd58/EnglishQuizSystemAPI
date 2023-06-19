using AutoMapper;
using EnglishQuizSystem.DTO;
using EnglishQuizSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
                    GradeForUser(temp.QuizId, temp.UserId);
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
                using (_context)
                {
                    UserQuiz userQuiz = _context.UserQuizzes.FirstOrDefault(x => x.UserId == userId && x.QuizId == quizId);
                    return (userQuiz == null ? NotFound() : Ok(_mapper.Map<UserQuizDTO>(userQuiz)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void GradeForUser(int quizId, int userId)
        {
            using (_context)
            {
                var query = @"SELECT 
                              COUNT(*) AS points 
                            FROM 
                              (
                                SELECT 
                              a.question_id,
                              STRING_AGG(a.id, ',') AS id
                            FROM 
                              answer AS a 
                            WHERE 
                              a.is_correct = 1 
                            GROUP BY 
                              a.question_id 
                              ) AS a 
                              INNER JOIN (
                                SELECT 
                                  question_id, 
                                  STRING_AGG(answer_id, ',') AS answer_id
                                FROM 
                                  user_answer AS ua 
                                WHERE 
                                  (quiz_id = @quiz_id) 
                                  AND (user_id = @user_id) 
                                GROUP BY 
                                  question_id
                              ) AS b ON a.question_id = b.question_id 
                              AND a.id = b.answer_id";

                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@quiz_id", quizId));
                command.Parameters.Add(new SqlParameter("@user_id", userId));
                _context.Database.OpenConnection();
                var points = (int)command.ExecuteScalar();
                _context.Database.CloseConnection();
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
