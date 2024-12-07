using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.User.Queries.LoginUserService
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IStoreContext _db;
        public LoginUserService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO<UserLoginDTO> Execute(LoginRequestDTO request, Guid? BrowserId)
        {
            var user = _db.Users
                .Include(c => c.UserInRoles)
                .ThenInclude(c => c.Role)
                .Where(c => c.Email.Equals(request.Email))
                .FirstOrDefault();

            if (user == null)
                return new ResultDTO<UserLoginDTO>()
                {
                    isSuccess = false,
                    Message = "کاربری با این مشخصات یافت نشد!",
                };

            bool Password = new PasswordHasher().VerifyPassword(user.Password, request.Password);
            if (!Password)
                return new ResultDTO<UserLoginDTO>()
                {
                    isSuccess = false,
                    Message = "رمزعبور نادرست است!",
                };

            List<string> roles = new List<string>();
            foreach (var item in user.UserInRoles)
            {
                roles.Add(item.Role.Name);
            }

            //Set Cart
            if (BrowserId != null)
            {
                var cart = _db.Carts
                    .Include(c => c.User)
                    .SingleOrDefault(c => c.BrowserId == BrowserId && c.UserId == null);
                if (cart != null)
                {
                    cart.User = user;
                    cart.UserId = user.Id;
                }
            }

            return new ResultDTO<UserLoginDTO>()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!",
                Data = new UserLoginDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Roles = roles
                }
            };
        }
    }
}
