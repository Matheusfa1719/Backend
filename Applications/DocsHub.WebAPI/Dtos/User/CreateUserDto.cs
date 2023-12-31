using DocsHub.Core.Enums;

namespace DocsHub.WebAPI.Dtos.User
{
    public class CreateUserDto
    {
        public required string  Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
    }
}