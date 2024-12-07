namespace Store.Application.Services.User.Commands.AddUserService
{
    public class RequestAddUserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RoleInAddUserDTO> Roles { get; set; }
    }
}
