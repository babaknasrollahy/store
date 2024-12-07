using Store.Common;

namespace Store.Application.Services.Products.Queries.GetCategoriesService
{
    public interface IGetCategoriesService
    {
        ResultDTO<List<CategoryDTO>> Execute(int? Id);
    }
}
