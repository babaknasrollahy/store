using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Products.Commands.AddProductService;
using Store.Common;
using Store.Domain.Entities.HomePage;

namespace Store.Application.Services.HomePage.Commads.AddImageService
{
    public class AddImageService : IAddImageService
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public AddImageService(IStoreContext db, IHostEnvironment environment)
        {
            _db = db;
            _en = environment;
        }
        public ResultDTO Execute(IFormFile file, ImageDTO image)
        {
            var src = UploadFile(file);
            var img = new HomePageImages()
            {
                ImageLocation = image.ImageLocation,
                Src = src.Path,
                Link = image.Link,
            };
            _db.HomePageImages.Add(img);
            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }
        private UploadFileDTO UploadFile(IFormFile file)
        {
            string folder = $@"images\SliderImages\";
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
