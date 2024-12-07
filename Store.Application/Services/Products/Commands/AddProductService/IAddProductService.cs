using Store.Common;

namespace Store.Application.Services.Products.Commands.AddProductService
{
    public interface IAddProductService
    {
        public ResultDTO Execute(RequestAddProductDTO request);
    }
}
