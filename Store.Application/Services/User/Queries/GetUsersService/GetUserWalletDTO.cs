namespace Store.Application.Services.User.Queries.GetUsersService;

public class GetUserWalletDTO
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
}