using Microsoft.AspNetCore.Mvc;

namespace EndPoint.WebSite.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
