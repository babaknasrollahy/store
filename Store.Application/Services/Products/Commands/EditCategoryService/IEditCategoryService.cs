using Store.Common;

namespace Store.Application.Services.Products.Commands.EditCategoryService
{
    public interface IEditCategoryService
    {
        ResultDTO Execute(int id, string name);
    }
}
