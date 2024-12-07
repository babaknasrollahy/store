using Store.Common;

namespace Store.Application.Services.User.Queries.LoginUserService
{
    public interface ILoginUserService
    {
        ResultDTO<UserLoginDTO> Execute(LoginRequestDTO request, Guid? BrowserId);
    }
}
