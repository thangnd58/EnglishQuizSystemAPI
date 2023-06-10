using AutoMapper;
using EnglishQuizSystem.DTO;
using EnglishQuizSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EnglishQuizSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuizController : ControllerBase
	{
		private readonly EnglishQuizSystemContext _context;
		private readonly IMapper _mapper;

		public QuizController(EnglishQuizSystemContext context, IMapper mapper)
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
					var listAllQuizzes = _context.Quizzes.ToList();
					return (listAllQuizzes == null ? NotFound() : Ok(_mapper.Map<IEnumerable<QuizDTO>>(listAllQuizzes)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpGet("{quizId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult Get(int quizId)
		{
			try
			{
				using (_context)
				{
					var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);
					return (quiz == null ? NotFound() : Ok(_mapper.Map<QuizDTO>(quiz)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Post([FromBody] QuizDTO quiz)
		{
			if (quiz == null) return BadRequest();
			try
			{
				using (_context)
				{
					_context.Quizzes.Add(_mapper.Map<Quiz>(quiz));
					_context.SaveChanges();
					return Ok("Insert Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Insert Failed: " + ex.Message);
			}
		}

		[HttpPut("{quizId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Put(int quizId, [FromBody] QuizDTO quiz)
		{
			if (quiz == null) return BadRequest();
			try
			{
				using (_context)
				{
					Quiz old = _context.Quizzes.FirstOrDefault(r => r.Id == quizId);
					if (old == null) return NotFound("Quiz does not exist.");
					old.Name = quiz.Name;
					old.CodeActive = quiz.CodeActive;
					old.Active = quiz.Active;
					_context.SaveChanges();
					return Ok("Update Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Update Failed: " + ex.Message);
			}
		}

		[HttpDelete("{quizId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Delete(int quizId)
		{
			try
			{
				using (_context)
				{
					Quiz quiz = _context.Quizzes.FirstOrDefault(r => r.Id == quizId);
					if (quiz == null) return NotFound("Quiz does not exist.");
					_context.Quizzes.Remove(quiz);
					_context.SaveChanges();
					return Ok("Delete Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Delete Failed: " + ex.Message);
			}
		}
	}
}
