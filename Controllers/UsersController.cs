using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NWRestfulAPI.Models;

namespace NWRestfulAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NorthwindContext _db;

        public UsersController(NorthwindContext db)
        {
            _db = db;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult AddUser([FromBody] User newUser)
        {
            if (newUser == null)
                return BadRequest("Invalid user data.");

            try
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating user: " + ex.Message);
            }
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.UserId)
                return BadRequest("User ID mismatch.");

            var existingUser = _db.Users.Find(id);
            if (existingUser == null)
                return NotFound($"User with ID {id} not found.");

            try
            {
                existingUser.Firstname = updatedUser.Firstname;
                existingUser.Lastname = updatedUser.Lastname;
                existingUser.Username = updatedUser.Username;
                existingUser.Password = updatedUser.Password;
                existingUser.Accesslevel = updatedUser.Accesslevel;

                _db.SaveChanges();
                return Ok($"✅ User {updatedUser.Username} updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error updating user: " + ex.Message);
            }
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            try
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return Ok($"🗑️ User {user.Username} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error deleting user: " + ex.Message);
            }
        }
    }
}