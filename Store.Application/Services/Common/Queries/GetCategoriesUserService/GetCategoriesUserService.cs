using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Common.Queries.GetCategoriesUserService
{
    public class GetCategoriesUserService : IGetCategoriesUserService
    {
        private readonly IStoreContext _db;
        public GetCategoriesUserService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<List<CategoryResultDTO>> Execute()
        {
            var Categories = _db.Categories
                .Include(c => c.ParentCategory)
                .Where(c => c.ParentCategoryId == null);
            return new ResultDTO<List<CategoryResultDTO>>()
            {
                isSuccess = true,
                Data = Categories.Select(c => new CategoryResultDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Children = c.ChildCategories.Select(ch => new CategoryResultDTO()
                    {
                        Id = ch.Id,
                        Name = ch.Name,

                    }).ToList(),
                }).ToList(),
            };
        }
    }
}
