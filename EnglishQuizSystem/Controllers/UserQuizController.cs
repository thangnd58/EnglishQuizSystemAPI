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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EnglishQuizSystem.Controllers
{
    public class ScoreStats
    {
        public int Score { get; set; }
        public int Count { get; set; }
    }


    [Route("api/[controller]")]
	[ApiController]
	public class UserQuizController : ControllerBase
	{
		private readonly EnglishQuizSystemContext _context;
		private readonly IMapper _mapper;

		public UserQuizController(EnglishQuizSystemContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		//[HttpGet]
		//[Authorize(AuthenticationSchemes = "Bearer")]
		//[EnableQuery]
		//public IActionResult Get()
		//{
		//	try
		//	{
		//		using (_context)
		//		{
		//			var listAllUserQuizzes = _context.UserQuizzes.ToList();
		//			return (listAllUserQuizzes == null ? NotFound() : Ok(_mapper.Map<IEnumerable<UserQuizDTO>>(listAllUserQuizzes)));
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest("Not found:" + ex.Message);
		//	}
		//}

		[HttpGet]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult GetPoints(int quizId, string username) {
			try
			{
				using (_context)
				{
					var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(username));
					var point = _context.UserQuizzes.FirstOrDefault(x => x.QuizId == quizId && x.UserId == user.Id).Score;
					return Ok(point);
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpGet("GetDataScore")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult GetDataScore()
		{
            try
            {
                using (_context)
                {
                    var query = @"SELECT score, COUNT(user_id) AS count FROM user_quiz GROUP BY score";
                    var command = _context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    _context.Database.OpenConnection();
                    var reader = command.ExecuteReader();

                    var scoreStats = new List<ScoreStats>();
                    while (reader.Read())
                    {
                        var score = reader.GetDouble(0);
                        var count = reader.GetInt32(1);
                        scoreStats.Add(new ScoreStats { Score = (int) score, Count = count });
                    }

                    _context.Database.CloseConnection();

                    return Ok(scoreStats);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Not found:" + ex.Message);
            }
        }
    }
}
