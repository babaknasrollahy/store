using Store.Common;

namespace Store.Application.Services.Products.Queries.GetCategoryByIdService
{
    public interface IGetCategoryByIdService
    {
        ResultDTO<CategoryDTO> Execute(int id);
    }
}
