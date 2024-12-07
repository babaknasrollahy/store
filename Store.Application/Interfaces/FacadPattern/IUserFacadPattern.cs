using Store.Application.Services.User.Commands.AddUserService;
using Store.Application.Services.User.Commands.DeleteUserService;
using Store.Application.Services.User.Commands.EditUserSerive;
using Store.Application.Services.User.Commands.UserStatusChangeService;
using Store.Application.Services.User.Queries.GetRoles;
using Store.Application.Services.User.Queries.GetUsersService;
using Store.Application.Services.User.Queries.LoginUserService;

namespace Store.Application.Interfaces.FacadPattern
{
    public interface IUserFacadPattern
    {
        public IAddUserService AddUser { get; }
        public IEditUserService EditUser { get; }
        public IDeleteUserService DeleteUser { get; }
        public IUserStatusChangeService UserStatus { get; }
        public IGetRolesService GetRoles { get; }
        public IGetUsersService GetUsers { get; }
        public ILoginUserService LoginUser { get; }
    }
}
