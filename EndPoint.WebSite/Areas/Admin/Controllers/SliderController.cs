using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPattern;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Operator")]
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IHomePageFacadPattern _db;
        public SliderController(IHomePageFacadPattern db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Slider.GetSliders.Execute().Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile File, string Link)
        {
            _db.Slider.AddSlider.Execute(new Store.Application.Services.HomePage.ISliderService.Commands.AddSliderService.SliderUploadFileDTO()
            {
                file = File,
                Link = Link
            });
            return Redirect(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _db.Slider.DeleteSlider.Execute(id);
            return Redirect(nameof(Index));
        }
    }
}
