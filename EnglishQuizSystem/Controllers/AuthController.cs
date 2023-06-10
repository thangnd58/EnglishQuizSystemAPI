using EnglishQuizSystem.DTO;
using EnglishQuizSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglishQuizSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		IConfiguration _configuration;
		private readonly EnglishQuizSystemContext _context;

		public AuthController(IConfiguration configuration, EnglishQuizSystemContext context)
		{
			_configuration = configuration;
			_context = context;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Auth([FromBody] AuthDTO auth)
		{
			IActionResult response = Unauthorized();
			var user = _context.Users.Include("Role").FirstOrDefault(u => u.UserName.Equals(auth.UserName) && u.Password.Equals(auth.Password));
			if (user != null)
			{
				var issuer = _configuration["Jwt:Issuer"];
				var audience = _configuration["Jwt:Audience"];
				var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
				var signingCredential = new SigningCredentials(
					new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature
					);
				var subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.UserName),
					new Claim(ClaimTypes.Role, user.Role.Name)
				});

				var expires = DateTime.UtcNow.AddDays(1);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = subject,
					Expires = expires,
					Issuer = issuer,
					Audience = audience,
					SigningCredentials = signingCredential
				};
				var tokenHandler = new JwtSecurityTokenHandler();
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var jwtToken = tokenHandler.WriteToken(token);
				var objReturn = new
				{
					token = jwtToken
				};
				return Ok(objReturn);
			}
			else
			{
				return NotFound("Invalid user name or password.");
			}
			return response;
		}
	}
}
