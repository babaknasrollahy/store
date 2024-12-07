using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.User.Commands.EditUserSerive
{
    public class EditUserService : IEditUserService
    {
        private readonly IStoreContext _db;
        public EditUserService(IStoreContext context)
        {
            _db = context;
        }
        public ResultDTO Execute(RequestEditUserDTO r)
        {
            var user = _db.Users.Find(r.id);
            if (user == null)
            {
                return new ResultDTO()
                {
                    isSuccess = false,
                    Message = "کاربر پیدا نشد!"
                };
            }

            user.Name = r.fullName;
            _db.SaveChanges();

            return new ResultDTO()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!"
            };
        }

    }
}
