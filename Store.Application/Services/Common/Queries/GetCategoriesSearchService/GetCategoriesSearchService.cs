using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Common.Queries.GetCategoriesSearchService
{
    public class GetCategoriesSearchService : IGetCategoriesSearchService
    {
        private readonly IStoreContext _db;
        public GetCategoriesSearchService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO<List<CategoryDTO>> Execute()
        {
            var categories = _db.Categories.Where(c => c.ParentCategoryId == null).ToList();
            return new ResultDTO<List<CategoryDTO>>()
            {
                isSuccess = true,
                Data = categories.Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList(),
            };
        }
    }
}
