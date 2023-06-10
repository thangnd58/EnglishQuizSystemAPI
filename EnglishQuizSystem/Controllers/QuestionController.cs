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
	public class QuestionController : ControllerBase
	{
		private readonly EnglishQuizSystemContext _context;
		private readonly IMapper _mapper;

		public QuestionController(EnglishQuizSystemContext context, IMapper mapper)
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
					var listAllQuestions = _context.Questions.ToList();
					return (listAllQuestions == null ? NotFound() : Ok(_mapper.Map<IEnumerable<QuestionDTO>>(listAllQuestions)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpGet("{questionId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult Get(int questionId)
		{
			try
			{
				using (_context)
				{
					var question = _context.Questions.FirstOrDefault(q => q.Id == questionId);
					return (question == null ? NotFound() : Ok(_mapper.Map<QuestionDTO>(question)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Post([FromBody] QuestionDTO question)
		{
			if (question == null) return BadRequest();
			try
			{
				using (_context)
				{
					_context.Questions.Add(_mapper.Map<Question>(question));
					_context.SaveChanges();
					return Ok("Insert Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Insert Failed: " + ex.Message);
			}
		}

		[HttpPut("{questionId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Put(int questionId, [FromBody] QuestionDTO question)
		{
			if (question == null) return BadRequest();
			try
			{
				using (_context)
				{
					Question old = _context.Questions.FirstOrDefault(r => r.Id == questionId);
					if (old == null) return NotFound("Question does not exist.");
					old.QuizId = question.QuizId;
					old.Text = question.Text;
					old.Active = question.Active;
					old.Type = question.Type;
					old.Difficulty = question.Difficulty;
					_context.SaveChanges();
					return Ok("Update Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Update Failed: " + ex.Message);
			}
		}

		[HttpDelete("{questionId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Delete(int questionId)
		{
			try
			{
				using (_context)
				{
					Question question = _context.Questions.FirstOrDefault(r => r.Id == questionId);
					if (question == null) return NotFound("Question does not exist.");
					_context.Questions.Remove(question);
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
