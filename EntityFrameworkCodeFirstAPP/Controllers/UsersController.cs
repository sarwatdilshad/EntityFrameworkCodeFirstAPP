using EntityFrameworkCodeFirstAPP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EntityFrameworkCodeFirstAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly UserContext userContext;

        public UsersController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await userContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var users = await userContext.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPost]
        public Users Post(Users users)
        {

            userContext.Users.Add(users);
            userContext.SaveChanges();
            return users;
        }

        [HttpPut("{id}")]
        public Users Put(int id, Users user)
        {
            if (user != null)
            {


                userContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                userContext.SaveChanges();
                return user;
            }
            else
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        public string DeleteUser(int id)
        {
            Users user = userContext.Users.Where(x => x.ID == id).FirstOrDefault();
            if (user != null)
            {
                userContext.Users.Remove(user);
                userContext.SaveChanges();
                return "User deleted";
            }
            else
            {
                return "No User Found";
            }
        }
    }
}
