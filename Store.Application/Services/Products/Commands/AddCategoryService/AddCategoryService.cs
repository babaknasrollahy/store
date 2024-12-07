using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddCategoryService
{
    public class AddCategoryService : IAddCategoryService
    {
        private readonly IStoreContext _db;
        public AddCategoryService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO Execute(int? categoryId, string categoryName)
        {
            Category category = new Category()
            {
                Name = categoryName,
                ParentCategory = GetParent(categoryId),
                InsertTime = DateTime.Now,
            };

            _db.Categories.Add(category);
            _db.SaveChanges();

            return new ResultDTO()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت تکمیل شد!"
            };
        }
        private Category GetParent(int? categoryId) => _db.Categories.Find(categoryId);
    }
}
