using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductAdminService
{
    public class GetProductAdmin : IGetProductAdmin
    {
        private readonly IStoreContext _db;
        public GetProductAdmin(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<ProductAdminDTO> Execute(int page, int pageSize)
        {
            int rowCount;
            var Products = _db.Products
                .Include(c => c.Category)
                .ThenInclude(c => c.ParentCategory)
                .ToPaged(page, pageSize, out rowCount)
                .ToList();
            return new ResultDTO<ProductAdminDTO>()
            {
                isSuccess = true,
                Message = " ",
                Data = new ProductAdminDTO()
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = rowCount,

                    Products = Products.Select(c => new ProductDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Brand = c.Brand,
                        Description = c.Description,
                        Displayed = c.Displayed,
                        Inventory = c.Inventory,
                        Price = c.Price,
                        Category = c.Category.ParentCategory.Name + " - " + c.Category.Name,
                    }).ToList(),
                }
            };
        }
    }
}
