using DocsHub.Core.Models;
using DocsHub.Core.Services.Interfaces;
using DocsHub.WebAPI.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace DocsHub.WebAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "user1", "user2" };
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAsync(Guid id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        // POST api/users
        [HttpPost]
        public async Task<User> PostAsync([FromBody] CreateUserDto newUser)
        {
            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = newUser.Role
            };
            user = await _userService.CreateUserAsync(user);
            return user;
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}