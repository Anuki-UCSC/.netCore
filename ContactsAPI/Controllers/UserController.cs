using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        public readonly ContactsAPIDbContext dbContext;

        public UserController(ContactsAPIDbContext contactsAPIDbContext)
        {
            this.dbContext=contactsAPIDbContext;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                var users = (from s in dbContext.Users select s).ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
        }


        [HttpPost]
        public IActionResult AddUser(User user)
        {
            try
            {
                var newUser = new User
                {
                    UserId = user.UserId,
                    age = user.age,
                    UserName = user.UserName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                };

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                return Ok(newUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                var UserRecord = (from s in dbContext.Users where s.UserId == user.UserId select s).First();
                UserRecord.UserName = user.UserName;
                UserRecord.age = user.age;
                UserRecord.Gender = user.Gender;
                UserRecord.DateOfBirth = user.DateOfBirth;
                dbContext.Users.Update(UserRecord);
                dbContext.SaveChanges();
                return Ok(UserRecord);
            }
            catch(Exception ex)
            {
               return BadRequest("Id not found");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] string id)
        {
            try
            {
                var user = (from s in dbContext.Users where s.UserId == id select s).First();
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return Ok(user);
            }
            catch(Exception ex)
            {
                return NotFound("Id not found!");
            }

        }
    }
}
