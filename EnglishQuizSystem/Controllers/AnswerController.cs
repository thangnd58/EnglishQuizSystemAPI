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
	public class AnswerController : ControllerBase
	{
		private readonly EnglishQuizSystemContext _context;
		private readonly IMapper _mapper;

		public AnswerController(EnglishQuizSystemContext context, IMapper mapper)
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
					var listAllAnswers = _context.Answers.ToList();
					return (listAllAnswers == null ? NotFound() : Ok(_mapper.Map<IEnumerable<AnswerDTO>>(listAllAnswers)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpGet("{answerId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult Get(int answerId)
		{
			try
			{
				using (_context)
				{
					var answer = _context.Answers.FirstOrDefault(q => q.Id == answerId);
					return (answer == null ? NotFound() : Ok(_mapper.Map<AnswerDTO>(answer)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Post([FromBody] AnswerDTO answer)
		{
			if (answer == null) return BadRequest();
			try
			{
				using (_context)
				{
					_context.Answers.Add(_mapper.Map<Answer>(answer));
					_context.SaveChanges();
					return Ok("Insert Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Insert Failed: " + ex.Message);
			}
		}

		[HttpPut("{answerId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Put(int answerId, [FromBody] AnswerDTO answer)
		{
			if (answer == null) return BadRequest();
			try
			{
				using (_context)
				{
					Answer old = _context.Answers.FirstOrDefault(r => r.Id == answerId);
					if (old == null) return NotFound("Answer does not exist.");
					old.QuestionId = answer.QuestionId;
					old.Text = answer.Text;
					old.IsCorrect = answer.IsCorrect;
					_context.SaveChanges();
					return Ok("Update Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Update Failed: " + ex.Message);
			}
		}

		[HttpDelete("{answerId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Delete(int answerId)
		{
			try
			{
				using (_context)
				{
					Answer answer = _context.Answers.FirstOrDefault(r => r.Id == answerId);
					if (answer == null) return NotFound("Answer does not exist.");
					_context.Answers.Remove(answer);
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
