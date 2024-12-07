using Store.Common;

namespace Store.Application.Services.Products.Queries.GetAllCatgories
{
    public interface IGetAllCategories
    {
        public ResultDTO<List<CategoryDTO>> Execute();
    }
}
