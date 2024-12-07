using Store.Common;

namespace Store.Application.Services.User.Commands.UserStatusChangeService
{
    public interface IUserStatusChangeService
    {
        public ResultDTO Execute(int Id);
    }
}
