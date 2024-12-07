using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductAdminService
{
    public interface IGetProductAdmin
    {
        ResultDTO<ProductAdminDTO> Execute(int page, int pageSize);
    }
}
