namespace Store.Application.Services.User.Queries.GetUsersService
{
    public interface IGetUsersService
    {
        public UserResultDTO Execute(RequestGetUserDTO request);
    }
}
