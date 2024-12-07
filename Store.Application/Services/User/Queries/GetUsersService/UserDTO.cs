namespace Store.Application.Services.User.Queries.GetUsersService
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public GetUserWalletDTO CompanyWallet { get; set; }
        public GetUserWalletDTO PersonalWallet { get; set; }
    }
}
