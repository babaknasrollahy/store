using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPattern;
using Store.Domain.Entities.HomePage;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Operator")]
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageFacadPattern _db;
        public HomePageController(IHomePageFacadPattern db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.GetAllImages.Execute().Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile file, ImageLocation Location, string Link)
        {
            _db.AddImage.Execute(file, new Store.Application.Services.HomePage.Commads.AddImageService.ImageDTO()
            {
                ImageLocation = Location,
                Link = Link
            });
            return View();
        }

        public IActionResult Delete(int Id)
        {
            _db.DeleteImage.Execute(Id);
            return RedirectToAction("Index");
        }
    }
}
