using EndPoint.WebSite.Models;
using EndPoint.WebSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.Products.Queries.GetProductUserService;
using System.Diagnostics;

namespace EndPoint.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageFacadPattern _db;
        private readonly IProductFacadPattern _product;
        public HomeController(ILogger<HomeController> logger, IHomePageFacadPattern db, IProductFacadPattern productFacadPattern)
        {
            _logger = logger;
            _db = db;
            _product = productFacadPattern;
        }

        public IActionResult Index()
        {
            var sliders = _db.Slider.GetSliders.Execute().Data;
            var imgs = _db.GetAllImages.Execute().Data;
            var hands = _product.GetProductUser.Execute(null, 20, Ordering.Newest, 1, 4);
            var mobs = _product.GetProductUser.Execute(null, 16, Ordering.Newest, 1, 4);
            var home = new HomePageViewModel()
            {
                Sliders = sliders,
                Images = imgs,
                HandsFree = hands.Data.products,
                Mobiles = mobs.Data.products,
            };
            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}