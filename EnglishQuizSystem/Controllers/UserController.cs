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
    public class UserController : ControllerBase
    {
        private readonly EnglishQuizSystemContext _context;
        private readonly IMapper _mapper;

        public UserController(EnglishQuizSystemContext context, IMapper mapper)
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
                    var listAllUsers = _context.Users.ToList();
                    return (listAllUsers == null ? NotFound() : Ok(_mapper.Map<IEnumerable<UserDTO>>(listAllUsers)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Not found:" + ex.Message);
            }
        }

        [HttpGet("{username}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get(string username)
        {
            try
            {
                using (_context)
                {
                    var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(username));
                    return (user == null ? NotFound() : Ok(_mapper.Map<UserDTO>(user)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Not found:" + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if (user == null) return BadRequest();
            try
            {
                using (_context)
                {
                    if (_context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName)) != null) 
                        return BadRequest("User name already exist.");
                    _context.Users.Add(_mapper.Map<User>(user));
                    _context.SaveChanges();
                    return Ok("Insert Successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Insert Failed: " + ex.Message);
            }
        }

        [HttpPut("{username}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Put(string username, [FromBody] UserDTO user)
        {
            if (user == null) return BadRequest();
            try
            {
                using (_context)
                {
                    User old = _context.Users.FirstOrDefault(u => u.UserName.Equals(username));
                    if (old == null) return NotFound("User does not exist.");
                    old.Password = user.Password;
                    old.RoleId = user.RoleId;
                    _context.SaveChanges();
                    return Ok("Update Successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update Failed: " + ex.Message);
            }
        }

        [HttpDelete("{username}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public IActionResult Delete(string username)
        {
            try
            {
                using (_context)
                {
                    User user = _context.Users.FirstOrDefault(u => u.UserName.Equals(username));
                    if (user == null) return NotFound("User does not exist.");
                    _context.Remove(user);
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
