using Store.Common;

namespace Store.Application.Services.User.Commands.AddUserService
{
    public interface IAddUserService
    {
        public ResultDTO<AddUserDTO> Execute(RequestAddUserDTO request, Guid? BrowserId);
    }
}
