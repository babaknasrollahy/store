using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductDetailUserService
{
    public class GetProductDetailUserService : IGetProductDetailUserSerivce
    {
        private readonly IStoreContext _db;
        public GetProductDetailUserService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<ProductDetailDTO> Execute(int id)
        {
            var Product = _db.Products
                 .Include(c => c.ProductImages)
                 .Include(c => c.ProductFeatures)
                 .Include(c => c.Category)
                 .ThenInclude(c => c.ParentCategory)
                 .Where(c => c.Id == id).FirstOrDefault();
            if (Product == null)
            {
                throw new Exception();
            }

            Product.Visit++;
            _db.SaveChanges();

            return new ResultDTO<ProductDetailDTO>()
            {
                isSuccess = true,
                Data = new ProductDetailDTO()
                {
                    Id = Product.Id,
                    Brand = Product.Brand,
                    Description = Product.Description,
                    Price = Product.Price,
                    Title = Product.Name,
                    ProductFeature = Product.ProductFeatures.Select(c => new ProductFeatureDTO()
                    {
                        Name = c.Name,
                        Value = c.Value,
                    }).ToList(),
                    Images = Product.ProductImages.Select(c => c.Src).ToList(),
                    Category = Product.Category.ParentCategory.Name + " - " + Product.Category.Name,
                }
            };
        }
    }
}
