using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductUserService
{
    public interface IGetProductUserService
    {
        ResultDTO<ResultProductUserDTO> Execute(string? SearchKey, int? CategoryId, Ordering ordering, int page, int pageSize);
    }
}
