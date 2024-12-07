using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.User.Commands.UserStatusChangeService
{
    public class UserStatusChangeService : IUserStatusChangeService
    {
        private readonly IStoreContext _db;
        public UserStatusChangeService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO Execute(int Id)
        {
            var user = _db.Users.Find(Id);
            if (user == null)
            {
                return new ResultDTO()
                {
                    isSuccess = false,
                    Message = "کاربر پیدا نشد!"
                };
            }

            user.IsActive = !user.IsActive;
            _db.SaveChanges();

            string s = user.IsActive == true ? "فعال" : "غیر فعال";
            return new ResultDTO()
            {
                isSuccess = true,
                Message = $"کاربر مورد نظر {s} شد"
            };
        }
    }
}
