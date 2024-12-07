namespace Store.Application.Services.User.Queries.LoginUserService
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
