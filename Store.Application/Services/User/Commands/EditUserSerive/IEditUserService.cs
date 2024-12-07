using Store.Common;

namespace Store.Application.Services.User.Commands.EditUserSerive
{
    public interface IEditUserService
    {
        ResultDTO Execute(RequestEditUserDTO r);
    }
}
