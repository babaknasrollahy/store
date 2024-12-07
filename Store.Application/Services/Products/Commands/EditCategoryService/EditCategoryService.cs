using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Commands.EditCategoryService
{
    public class EditCategoryService : IEditCategoryService
    {
        private readonly IStoreContext _db;
        public EditCategoryService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO Execute(int id, string name)
        {
            var c = _db.Categories.Find(id);
            if (c == null)
                return new ResultDTO()
                {
                    isSuccess = false,
                    Message = "دسته بندی یافت نشد!"
                };
            c.Name = name;
            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!",
            };
        }
    }
}
