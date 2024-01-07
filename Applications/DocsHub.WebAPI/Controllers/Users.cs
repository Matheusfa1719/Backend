using DocsHub.Core.Common;
using DocsHub.Core.Models;
using DocsHub.Core.Services.Interfaces;
using DocsHub.WebAPI.Dtos;
using DocsHub.WebAPI.Dtos.User;
using DocsHub.WebAPI.Validators;
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
        public async Task<ActionResult<PagedList<User>>> GetAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            return await _userService.GetAllUsersAsync(pageIndex, pageSize);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<User> { Success = false, StatusCode = 404, Message = "Usuário não encontrado" });
            }

            user.Password = "";
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserDto newUser)
        {
            var validator = new CreateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(newUser);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<List<string>> { Success = false, StatusCode = 400, Message = "Validation errors", Data = errors });
            }

            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = newUser.Role
            };

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            var userResult = await _userService.CreateUserAsync(user);
            if (userResult.IsError)
            {
                return BadRequest(new ApiResponse<User> { Success = false, StatusCode = 400, Message = userResult.Error });
            }

            if (userResult.Value == null)
            {
                return BadRequest(new ApiResponse<User> { Success = false, StatusCode = 400, Message = "Erro ao criar usuário" });
            }

            userResult.Value.Password = "";
            return Ok(new ApiResponse<User> { Success = true, StatusCode = 200, Message = "Usuário cadastrado com sucesso!", Data = userResult.Value });
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] UpdateUserDto updatedUserDto)
        {
            var validator = new UpdateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(updatedUserDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<List<string>> { Success = false, StatusCode = 400, Message = "Validation errors", Data = errors });
            }

            var user = new User
            {
                Id = id,
                Name = updatedUserDto.Name,
                Email = updatedUserDto.Email,
                Role = updatedUserDto.Role,
            };

            var updateUserResult = await _userService.UpdateUserAsync(user);
            if (updateUserResult.IsError)
            {
                if (updateUserResult.Error == "Usuário não encontrado")
                {
                    return NotFound(new ApiResponse<User> { Success = false, StatusCode = 404, Message = "Usuário não encontrado" });
                }
                else
                {
                    return BadRequest(new ApiResponse<User> { Success = false, StatusCode = 400, Message = updateUserResult.Error });
                }
            }

            return Ok(new ApiResponse<User> { Success = true, StatusCode = 200, Message = "Usuário atualizado com sucesso!", Data = updateUserResult.Value });
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteUserResult = await _userService.DeleteUserByIdAsync(id);
            if (deleteUserResult.IsError)
            {
                if (deleteUserResult.Error == "Usuário não encontrado")
                {
                    return NotFound(new ApiResponse<User> { Success = false, StatusCode = 404, Message = "Usuário não encontrado" });
                }
                else
                {
                    return BadRequest(new ApiResponse<User> { Success = false, StatusCode = 400, Message = deleteUserResult.Error });
                }
            }

            return NoContent();
        }
    }
}