using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Commands.AddProductService
{
    public class AddProductService : IAddProductService
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public AddProductService(IStoreContext context, IHostEnvironment environment)
        {
            _db = context;
            _en = environment;
        }
        public ResultDTO Execute(RequestAddProductDTO request)
        {
            var category = _db.Categories.Find(request.CategoryId);
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Brand = request.Brand,
                Price = request.Price,
                Inventory = request.Inventory,
                Displayed = request.Displayed,
                Category = category,
            };
            _db.Products.Add(product);

            List<ProductFeature> features = new List<ProductFeature>();
            foreach (var item in request.ProductFeatures)
            {
                features.Add(new()
                {
                    Name = item.Name,
                    Value = item.Value,
                    Product = product,
                });
            }
            _db.ProductFeatures.AddRange(features);

            List<ProductImage> images = new List<ProductImage>();
            foreach (var item in request.ProductImages)
            {
                var uploaded = UploadFile(item);
                images.Add(new ProductImage
                {
                    Product = product,
                    Src = uploaded.Path,
                });
            }
            _db.ProductImages.AddRange(images);
            _db.SaveChanges();

            return new ResultDTO()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت تکمیل شد!",
            };

        }

        private UploadFileDTO UploadFile(IFormFile file)
        {
            string folder = $@"images\ProductImages\";
            var uploadsRootFolder = Path.Combine(_en.ContentRootPath, "wwwroot", folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            if (file == null || file.Length == 0)
            {
                return new UploadFileDTO()
                {
                    Status = false,
                    Path = "",
                };
            }

            string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
            var filePath = Path.Combine(uploadsRootFolder, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return new UploadFileDTO()
            {
                Status = true,
                Path = folder + fileName,
            };
        }
    }
}
