using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductDetailAdminService;

public interface IGetProductDetailAdminService
{
    ResultDTO<ProductDetailDTO> Execute(int id);
}