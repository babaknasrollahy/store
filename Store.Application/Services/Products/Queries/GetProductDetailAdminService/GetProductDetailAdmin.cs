using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductDetailAdminService
{
    public class GetProductDetailAdmin : IGetProductDetailAdminService
    {
        private readonly IStoreContext _db;
        public GetProductDetailAdmin(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO<ProductDetailDTO> Execute(int id)
        {
            var product = _db.Products.Where(c => c.Id == id)
                .Include(c => c.Category)
                .ThenInclude(c => c.ParentCategory)
                .Include(c => c.ProductImages)
                .Include(c => c.ProductFeatures)
                .FirstOrDefault();
            if (product == null)
            {
                return new ResultDTO<ProductDetailDTO>()
                {
                    isSuccess = false,
                    Message = "کاربر یافت نشد!",
                };
            }

            return new ResultDTO<ProductDetailDTO>()
            {
                isSuccess = true,
                Message = " ",
                Data = new ProductDetailDTO()
                {
                    Id = id,
                    Brand = product.Brand,
                    Category = product.Category.ParentCategory.Name + " - " + product.Category.Name,
                    Name = product.Name,
                    Description = product.Description,
                    Displayed = product.Displayed,
                    Inventory = product.Inventory,
                    Price = product.Price,
                    ProductFeatures = product.ProductFeatures.Select(c => new ProductFeatrueDetailDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ProductId = c.Id,
                        Value = c.Value,
                    }).ToList(),
                    ProductImages = product.ProductImages.Select(c => new ProductImageDetailDTO()
                    {
                        Id = c.Id,
                        ProductId = c.ProductId,
                        Src = c.Src,
                    }).ToList(),
                }
            };
        }
    }
}
