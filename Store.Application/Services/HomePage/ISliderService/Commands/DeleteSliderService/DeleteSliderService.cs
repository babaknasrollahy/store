using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.HomePage.ISliderService.Commands.DeleteSliderService
{
    public class DeleteSliderService : IDeleteSliderService
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public DeleteSliderService(IStoreContext db, Microsoft.Extensions.Hosting.IHostEnvironment en)
        {
            _db = db;
            _en = en;
        }

        public ResultDTO Execute(int id)
        {
            var Slider = _db.Slides.Find(id);
            if (Slider == null)
                return new ResultDTO()
                {
                    isSuccess = false,
                };

            _db.Slides.Remove(Slider);

            var file = Path.Combine(_en.ContentRootPath, "wwwroot", Slider.Src);
            System.IO.File.Delete(file);

            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }
    }
}
