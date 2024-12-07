using Store.Common;

namespace Store.Application.Services.Products.Commands.DeleteProductService
{
    public interface IDeleteProductService
    {
        ResultDTO Execute(int Id);
    }
}
