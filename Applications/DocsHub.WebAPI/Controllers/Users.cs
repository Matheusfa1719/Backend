using DocsHub.Core.Models;
using DocsHub.Core.Services.Interfaces;
using DocsHub.WebAPI.Dtos;
using DocsHub.WebAPI.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocsHub.WebAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UsersController(IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
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
        public async Task<IActionResult> PostAsync([FromBody] CreateUserDto newUser)
        {
            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = newUser.Role
            };

            //TODO: Validar dados de entrada e salvar senha encryptada no banco de dados
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            var userResult = await _userService.CreateUserAsync(user);
            if (userResult.IsError)
            {
                return BadRequest(new ApiResponse<User> { Success = false, StatusCode = 400, Message = userResult.Error});
            }
            
            if (userResult.Value == null)
            {
                return BadRequest(new ApiResponse<User> { Success = false, StatusCode = 400, Message = "Erro ao criar usuário"});
            }

            userResult.Value.Password = ""; 
            return Ok(new ApiResponse<User> { Success = true, StatusCode = 200, Message = "Usuário cadastrado com sucesso!",Data = userResult.Value });
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