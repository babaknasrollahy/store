using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.User.Commands.AddUserService;
using Store.Application.Services.User.Commands.DeleteUserService;
using Store.Application.Services.User.Commands.EditUserSerive;
using Store.Application.Services.User.Commands.UserStatusChangeService;
using Store.Application.Services.User.Queries.GetRoles;
using Store.Application.Services.User.Queries.GetUsersService;
using Store.Application.Services.User.Queries.LoginUserService;

namespace Store.Application.Services.User.UserFacadPattern
{
    public class UserFacadPattern : IUserFacadPattern
    {
        private readonly IStoreContext _context;
        public UserFacadPattern(IStoreContext context)
        {
            _context = context;
        }

        private AddUserService _addUser;
        public IAddUserService AddUser => _addUser = _addUser ?? new AddUserService(_context);

        private EditUserService _editUser;
        public IEditUserService EditUser => _editUser = _editUser ?? new EditUserService(_context);

        private DeleteUserService _deleteUser;
        public IDeleteUserService DeleteUser => _deleteUser = _deleteUser ?? new DeleteUserService(_context);

        private UserStatusChangeService _userStatus;
        public IUserStatusChangeService UserStatus => _userStatus = _userStatus ?? new UserStatusChangeService(_context);

        private GetRolesService _getRoles;
        public IGetRolesService GetRoles => _getRoles = _getRoles ?? new GetRolesService(_context);

        private GetUsersService _getUsers;
        public IGetUsersService GetUsers => _getUsers = _getUsers ?? new GetUsersService(_context);

        private LoginUserService _loginUser;
        public ILoginUserService LoginUser => _loginUser = _loginUser ?? new LoginUserService(_context);
    }
}
