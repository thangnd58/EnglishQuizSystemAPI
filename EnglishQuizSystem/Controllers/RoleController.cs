using AutoMapper;
using EnglishQuizSystem.DTO;
using EnglishQuizSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EnglishQuizSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly EnglishQuizSystemContext _context;
		private readonly IMapper _mapper;

		public RoleController(EnglishQuizSystemContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		[EnableQuery]
		public IActionResult Get()
		{
			try
			{
				using (_context)
				{
					var listAllRoles = _context.Roles.ToList();
					return (listAllRoles == null ? NotFound() : Ok(_mapper.Map<IEnumerable<RoleDTO>>(listAllRoles)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found: " + ex.Message);
			}
		}

		[HttpGet("{roleId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public IActionResult Get(int roleId)
		{
			try
			{
				using (_context)
				{
					var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
					return (role == null ? NotFound() : Ok(_mapper.Map<RoleDTO>(role)));
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Not found:" + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Post([FromBody] RoleDTO role)
		{
			if (role == null) return BadRequest();
			try
			{
				using (_context)
				{
					_context.Roles.Add(_mapper.Map<Role>(role));
					_context.SaveChanges();
					return Ok("Insert Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Insert Failed: " + ex.Message);
			}
		}

		[HttpPut("{roleId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Put(int roleId, [FromBody] RoleDTO role)
		{
			if (role == null) return BadRequest();
			try
			{
				using (_context)
				{
					Role old = _context.Roles.FirstOrDefault(r => r.Id == roleId);
					if (old == null) return NotFound("Role does not exist.");
					old.Name = role.Name;
					_context.SaveChanges();
					return Ok("Update Successfully.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Update Failed: " + ex.Message);
			}
		}

		[HttpDelete("{roleId}")]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
		public IActionResult Delete(int roleId)
		{
			try
			{
				using (_context)
				{
					Role role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
					if (role == null) return NotFound("Role does not exist.");
					_context.Roles.Remove(role);
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
