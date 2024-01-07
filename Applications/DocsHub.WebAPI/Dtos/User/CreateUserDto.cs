namespace DocsHub.WebAPI.Dtos.User
{
    public class CreateUserDto : UserDto
    {
        public required string Password { get; set; }
    }
}