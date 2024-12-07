using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.User.Commands.DeleteUserService
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IStoreContext _db;
        public DeleteUserService(IStoreContext context)
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
                    Message = "کاربر مورد نظر یافت نشد!"
                };
            }

            user.IsRemoved = true;
            user.RemoveTime = DateTime.Now;
            _db.SaveChanges();

            return new ResultDTO()
            {
                isSuccess = true,
                Message = "کاربر مورد نظر شما با موفقیت حذف شد"
            };
        }
    }
}
