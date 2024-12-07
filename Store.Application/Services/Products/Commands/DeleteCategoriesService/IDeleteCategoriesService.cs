using Store.Common;

namespace Store.Application.Services.Products.Commands.DeleteCategoriesService
{
    public interface IDeleteCategoriesService
    {
        ResultDTO Execute(int Id);
    }
}
