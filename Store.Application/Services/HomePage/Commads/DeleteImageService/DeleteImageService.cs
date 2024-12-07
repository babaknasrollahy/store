using Microsoft.Extensions.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.HomePage.Commads.DeleteImageService
{
    public class DeleteImageService : IDeleteImageService
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public DeleteImageService(IStoreContext db, IHostEnvironment environment)
        {
            _db = db;
            _en = environment;
        }
        public ResultDTO Execute(int Id)
        {
            var img = _db.HomePageImages.Find(Id);
            var uploadsRootFolder = Path.Combine(_en.ContentRootPath, "wwwroot", img.Src);
            File.Delete(uploadsRootFolder);
            _db.HomePageImages.Remove(img);
            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }
    }
}
