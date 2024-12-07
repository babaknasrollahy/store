using EndPoint.Site.Utilities;
using EndPoint.WebSite.Units;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Cart;

namespace EndPoint.WebSite.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly ICartService _db;
        private readonly CookieManeger _cookie;
        public Cart(ICartService db)
        {
            _db = db;
            _cookie = new CookieManeger();
        }
        public IViewComponentResult Invoke()
        {
            return View(viewName: "Cart", model: _db.GetCart(_cookie.GetBrowserId(HttpContext), ClaimUtility.GetUserId(User)).Data);
        }
    }
}
