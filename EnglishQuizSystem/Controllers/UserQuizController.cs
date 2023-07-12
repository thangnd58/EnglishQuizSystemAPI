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

    public class TopAvgPoint
    {
        public string UserName { get; set; }
        public float AvgScore { get; set; }
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

		[HttpGet("GetTopAvgPoint")]
		public IActionResult GetTopAvgPoint()
		{
			try
			{
				using (_context)
				{
					var query = @"SELECT top(10) u.user_name,
									   round(uq.avg_point, 2) as avg_point
								FROM [user] u
								JOIN
								  (SELECT user_id,
										  sum(score)/count(score) AS avg_point
								   FROM user_quiz
								   GROUP BY user_id) uq ON u.id = uq.user_id
								ORDER BY uq.avg_point DESC;";
					var command = _context.Database.GetDbConnection().CreateCommand();
					command.CommandText = query;
					_context.Database.OpenConnection();
					var reader = command.ExecuteReader();

					var topAvgPoints = new List<TopAvgPoint>();
					while (reader.Read())
					{
						var username = reader.GetString(0);
						var avgpoint = reader.GetDouble(1);
						topAvgPoints.Add(new TopAvgPoint { UserName = username, AvgScore = (float) avgpoint});
					}

					_context.Database.CloseConnection();

					return Ok(topAvgPoints);
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}
	}
}
