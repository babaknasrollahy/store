using Store.Common;

namespace Store.Application.Services.User.Commands.DeleteUserService
{
    public interface IDeleteUserService
    {
        ResultDTO Execute(int Id);
    }
}
