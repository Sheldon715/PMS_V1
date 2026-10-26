using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.API.DTOs;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetUsers(
            List<User> users,
            [FromQuery] int pageSize,
            [FromQuery] int pageNumber
        )
        {
            return users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }

        [HttpGet]
        public List<User> GetUserById(
            [FromQuery] int id, 
            List<User> users)
        {
            return users
            .Where(u => u.Id == id)
            .ToList();
        }

        [HttpGet]
        public List<User> GetUserByName(
            [FromQuery] string name, 
            List<User> users)
        {
            return users
            .Where(u => u.Name == name)
            .ToList();
        }

        [HttpGet]
        public List<User> GetUserByEmail(
            [FromQuery] string email, 
            List<User> users)
        {
            return users
            .Where(u => u.Email == email)
            .ToList();
        }

        [HttpPost]
        public ActionResult<User> createUser([FromBody] CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email)) return BadRequest();
            
            if (_context.Users.FirstOrDefault(u => u.Email == request.Email) != null) return BadRequest();

            try
            {
                var email = new System.Net.Mail.MailAddress(request.Email);
            }
            catch
            {
                return BadRequest();
            }

            var newUser = new User();

            newUser.Name = request.Name;
            newUser.Email = request.Email;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new {id = newUser.Id}, newUser);
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserResponseDTO>> GetAllUsers()
        {
            var allUser = _context.Users
            .Select(u => new UserResponseDTO
            {
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone

            })
            .ToList();

            return Ok(allUser);
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseDTO> GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            var response = new UserResponseDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser([FromBody]UpdateUserRequest request, int id)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || 
            string.IsNullOrWhiteSpace(request.Email) || 
            string.IsNullOrWhiteSpace(request.Phone))
            {
                return BadRequest();
            }

            try
            {
                var email = new System.Net.Mail.MailAddress(request.Email);
            }
            catch
            {
                return BadRequest();
            }

            var updateUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (updateUser == null) return NotFound();

            updateUser.Name = request.Name;
            updateUser.Email = request.Email;
            updateUser.Phone = request.Phone;
            _context.SaveChanges();

            return Ok(updateUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
