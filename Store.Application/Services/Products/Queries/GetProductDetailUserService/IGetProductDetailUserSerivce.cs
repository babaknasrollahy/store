using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductDetailUserService
{
    public interface IGetProductDetailUserSerivce
    {
        ResultDTO<ProductDetailDTO> Execute(int id);
    }
}
