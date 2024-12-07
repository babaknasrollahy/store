using Store.Common;

namespace Store.Application.Services.Products.Commands.AddCategoryService
{
    public interface IAddCategoryService
    {
        public ResultDTO Execute(int? categoryId, string categoryName);
    }
}
