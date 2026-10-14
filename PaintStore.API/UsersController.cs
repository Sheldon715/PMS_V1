using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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
    }
}
