using EndPoint.Site.Utilities;
using EndPoint.WebSite.Units;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Cart;
using Store.Application.Services.Wallet.Shared.Queries.GetWalletsBalanceByUserService;

namespace EndPoint.WebSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _db;
        private readonly IGetWalletsBalanceByUserService _getWalletsBalanceByUserService;
        private readonly CookieManeger _cookie;
        public CartController(ICartService db, IGetWalletsBalanceByUserService getWalletsBalanceByUserService)
        {
            _db = db;
            _cookie = new CookieManeger();
            _getWalletsBalanceByUserService = getWalletsBalanceByUserService;
        }

        public async Task<IActionResult> Index()
        {
            var balances = await _getWalletsBalanceByUserService.ExecuteAsync(ClaimUtility.GetUserId(User).Value);
            ViewBag.CompanyBalance = balances.Data.CompanyBalance;
            ViewBag.PersonalBalance = balances.Data.PersonalBalance;

            var cart = _db.GetCart(_cookie.GetBrowserId(HttpContext), ClaimUtility.GetUserId(User));
            ViewBag.TotalAmount = cart.Data.TotalAmount;

            return View(cart.Data);
        }

        public IActionResult Add(int productId)
        {

            _db.AddToCart(productId, _cookie.GetBrowserId(HttpContext), ClaimUtility.GetUserId(User));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int itemId)
        {
            _db.DeleteFromCart(itemId);
            return Json(true);
        }

        [HttpPost]
        public IActionResult Increase(int itemId)
        {
            _db.IncreaseCartItem(itemId);
            return Json(true);
        }

        [HttpPost]
        public IActionResult Decrease(int itemId)
        {
            _db.LowFromCart(itemId);
            return Json(true);
        }

    }
}
