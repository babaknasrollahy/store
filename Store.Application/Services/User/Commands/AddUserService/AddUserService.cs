using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Users;
using Store.Domain.Entities.Wallet;

namespace Store.Application.Services.User.Commands.AddUserService
{
    public class AddUserService : IAddUserService
    {
        private readonly IStoreContext _db;
        public AddUserService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO<AddUserDTO> Execute(RequestAddUserDTO request, Guid? BrowserId)
        {
            try
            {
                var HashedPassword = new PasswordHasher().HashPassword(request.Password);
                var user = new Domain.Entities.Users.User()
                {
                    Email = request.Email,
                    Name = request.FullName,
                    Password = HashedPassword,
                    IsActive = true,
                };

                var cWallet = new CompanyWallet() { Id = Guid.NewGuid(), User = user, UserId = user.Id };
                var pWallet = new PersonalWallet() { Id = Guid.NewGuid(), User = user, UserId = user.Id };
                user.CompanyWallet = cWallet;
                user.CompanyWalletId = cWallet.Id;
                user.PersonalWallet = pWallet;
                user.PersonalWalletId = pWallet.Id;

                List<UserInRole> Roles = new List<UserInRole>();
                foreach (var item in request.Roles)
                {
                    var role = _db.Roles.Find(item.Id) ?? throw new Exception();
                    Roles.Add(new UserInRole
                    {
                        Role = role,
                        RoleId = role.Id,
                        User = user,
                        UserId = user.Id
                    });
                }
                user.UserInRoles = Roles;

                _db.Users.Add(user);
                _db.CompanyWallets.Add(cWallet);
                _db.PersonalWallets.Add(pWallet);

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

                _db.SaveChanges();

                return new ResultDTO<AddUserDTO>()
                {
                    Data = new AddUserDTO() { Id = user.Id },
                    isSuccess = true,
                    Message = "عملیات با موفقیت انجام شد!"
                };

            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return new ResultDTO<AddUserDTO>()
                {
                    isSuccess = false,
                    Message = "ایمیل ورودی شما در سایت موجود است!"
                };
            }
        }
    }
}

