using Store.Common;

namespace Store.Application.Services.Common.Queries.GetCategoriesSearchService
{
    public interface IGetCategoriesSearchService
    {
        ResultDTO<List<CategoryDTO>> Execute();
    }
}
