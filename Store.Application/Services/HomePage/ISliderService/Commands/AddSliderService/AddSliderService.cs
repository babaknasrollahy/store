using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Products.Commands.AddProductService;
using Store.Common;

namespace Store.Application.Services.HomePage.ISliderService.Commands.AddSliderService
{
    public class AddSliderService : IAddSliderService
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public AddSliderService(IStoreContext db, Microsoft.Extensions.Hosting.IHostEnvironment en)
        {
            _db = db;
            _en = en;
        }

        public ResultDTO Execute(SliderUploadFileDTO slider)
        {
            var s = new Domain.Entities.HomePage.Slides()
            {
                Link = slider.Link,
            };
            var up = UploadFile(slider.file);
            s.Src = up.Path;

            _db.Slides.Add(s);
            _db.SaveChanges();
            return new ResultDTO() { isSuccess = true };
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
