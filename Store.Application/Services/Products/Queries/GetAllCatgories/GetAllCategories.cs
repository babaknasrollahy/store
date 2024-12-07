using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetAllCatgories
{
    public class GetAllCategories : IGetAllCategories
    {
        private readonly IStoreContext _db;
        public GetAllCategories(IStoreContext context)
        {
            _db = context;
        }
        public ResultDTO<List<CategoryDTO>> Execute()
        {
            var categories = _db.Categories
                .Include(c => c.ParentCategory)
                .Where(c => c.ParentCategoryId != null).ToList();

            List<CategoryDTO> result = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                result.Add(new CategoryDTO()
                {
                    Id = item.Id,
                    Name = item.Name + " - " + item.ParentCategory.Name,
                });
            }

            return new ResultDTO<List<CategoryDTO>>()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت تکمیل شد!;",
                Data = result,
            };
        }
    }
}
