using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetCategoriesService
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IStoreContext _db;
        public GetCategoriesService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO<List<CategoryDTO>> Execute(int? Id)
        {
            var Categoies = _db.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.ChildCategories)
                .Where(c => c.ParentCategoryId == Id)
                .Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    parentCategory = c.ParentCategory != null ? new ParentCategoryDTO()
                    {
                        Id = c.ParentCategory.Id,
                        Name = c.ParentCategory.Name,
                    } : null,
                    HasChild = c.ChildCategories.Any()
                }).ToList();
            return new ResultDTO<List<CategoryDTO>>()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!",
                Data = Categoies,
            };
        }
    }
}
