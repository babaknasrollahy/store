using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Commands.DeleteCategoriesService
{
    public class DeleteCategoriesService : IDeleteCategoriesService
    {
        private readonly IStoreContext _db;
        public DeleteCategoriesService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO Execute(int Id)
        {
            var Category = _db.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.ChildCategories)
                .FirstOrDefault(c => c.Id == Id);
            if (Category == null)
                return new ResultDTO() { isSuccess = false, Message = "دسته بندی یافت نشد!" };
            if (Category.ParentCategory == null)
                if (Category.ChildCategories != null)
                    _db.Categories.RemoveRange(Category.ChildCategories);
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
                Message = "دسته بندی مورد نظر به طور کامل حذف شد!",
            };
        }
    }
}
